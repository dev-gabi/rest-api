using Entities;
using Entities.Config;
using Entities.Response;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dal
{
    public class NamesDal
    {
        #region ctor
        public NamesDal(NamesContext context, IOptions<ActionTypes> actions)
        {
            _context = context;
            _actions = actions.Value;
        }
        #endregion

        #region properties
        private NamesContext _context;
        private readonly ActionTypes _actions;
        #endregion


        #region public
        public Response AddName(Name name)
        {
            try
            {
                _context.Names.Add(name);
                _context.SaveChanges();

                return GetResponse(name.Id, _actions.Add);
            }
            catch (Exception x)
            {
                return GetErrorResponse(x);
            }
        }

        public NameResponse[] GetNamesWithPersonId()
        {
           return _context.Names
                .Select(name => new NameResponse
                {
                    Id = name.Id,
                    FirstName = name.FirstName,
                    LastName = name.LastName,
                    NickName = name.NickName,
                    PersonId = name.Person.Id
                })
                .ToArray();

        }

        public Response UpdateName(Name name)
        {       
            try
            {
                _context.Entry(name).State = EntityState.Modified; //only the Name entity will be modified
              //  _context.Names.Update(name);    
                _context.SaveChanges ();
                return GetResponse(name.Id, _actions.Update);
            }
            catch (Exception x)
            {
                return GetErrorResponse(x);
            }
        }

        public Response UpdatePatchName(int id, JsonPatchDocument patch)
        {
            try
            {
                Name name =  _context.Names.Find(id);
                if(name != null)
                {
                    patch.ApplyTo(name);
                    _context.SaveChanges () ;
                    return GetResponse(name.Id, _actions.Patch);
                }
                throw new Exception($"Name for id: {id} was not found");
            }
            catch (Exception x)
            {
                return GetErrorResponse(x);
            }
        }

        public Response DeleteName(int id)
        {
            try
            {
                Name name = _context.Names.Find(id);  
                if(name != null)
                {
                   _context.Names.Remove(name);
                   _context.SaveChanges();                
                }
                return GetDeleteResponse(name?.Id);
            }
            catch (Exception x)
            {
                return GetErrorResponse(x);
            }
        }

        #endregion

        #region private
        private Response GetDeleteResponse(int? id)
        {
            Response res = new();
            if(id != null)
            {
                Name deletedName = _context.Names.Find(id);
                if (deletedName == null) SetDeleteSuccessResponse(res);
                else SetDeleteErrorResponse(res);
            }
            else
            {
                SetDeleteErrorResponse(res);
            }
            return res;
        }

        void SetDeleteSuccessResponse(Response res)
        {
            res.StatusCode = 200;
            res.StatusDescription = $"{_actions.Delete} successfully";
        }

        void SetDeleteErrorResponse(Response res)
        {
            res.StatusCode = 99;
            res.StatusDescription = "something went wrong in db";
        }

        private Response GetResponse(int id, string action)
        {
            Response res = new();
            Name addedName = _context.Names.Find(id);
            if (addedName != null)
            {
                res.StatusCode = 200;
                res.StatusDescription = $"{action} successfully";
            }
            else
            {
                res.StatusCode = 99;
                res.StatusDescription = "something went wrong in db";
            }
            return res;
        }

        private Response GetErrorResponse(Exception x)
        {
            return  new Response() {
                StatusCode = 500,
                StatusDescription = x.Message + x.InnerException
              };
        }
        #endregion

    }
}

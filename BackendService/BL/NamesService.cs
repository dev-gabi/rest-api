using Dal;
using Entities;
using Entities.Request;
using Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System.Xml.Linq;

namespace BL
{
    public class NamesService : INameService
    {
        private  NamesDal _dal;
        public NamesService(NamesDal dal)
        {
            _dal = dal;
        }

        public Response AddName(AddNameReq name)
        {
            try
            {
                return _dal.AddName(name.ConvertToName());
            }
            catch (Exception x)
            {
                return HandleException(x);
            }
        }

        public NamesResponse GetNamesWithPersonId()
        {
            NamesResponse res = new();

            try
            {
                NameResponse[] names = _dal.GetNamesWithPersonId();
                if (names?.Length == 0)
                {
                    res.StatusCode = StatusCodes.Status200OK;
                    res.StatusDescription = "No names where found";
                }
                else
                {
                    res.StatusCode = StatusCodes.Status200OK;
                    res.NamesArray = names;
                }
            }
            catch (Exception x)
            {
                return HandleException(x).ConvertToNamesResponse();
            }
            return res;
        }

        public Response UpdateName(UpdateNameReq name)
        {
            try
            {
                return _dal.UpdateName(name.ConvertToName());
            }
            catch (Exception x)
            {
               return HandleException(x);
            }
        }

        public Response UpdatePatchName(int id, JsonPatchDocument name)
        {
            try
            {
                return _dal.UpdatePatchName(id, name);
            }
            catch (Exception x)
            {
                return HandleException(x);
            }
        }

        public Response DeleteName(int id)
        {
            try
            {
                return _dal.DeleteName(id);
            }
            catch (Exception x)
            {
                return HandleException(x);
            }
        }
        #region private
        Response HandleException(Exception x)
        {
            return new Response()
            {
                StatusCode = 500,
                 StatusDescription = x.Message
            };
        }
        #endregion
    }
}
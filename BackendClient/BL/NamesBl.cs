using Entities;
using Entities.NamesSa;
using Entities.Request;
using Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using SA;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace BL
{
    public class NamesBl
    {
        private readonly INamesSa _nameSa;

        public NamesBl(INamesSa nameSa)
        {
            _nameSa = nameSa;
        }

        public async Task<Response> PostNameAsync(PostNameRequest name)
        {
            try
            {
                if (name == null) throw new ArgumentNullException("name");
                return await _nameSa.PostNameAsync(name);
            }
            catch (Exception x)
            {
                return new Response() { StatusCode = (int)StatusCodes.Status400BadRequest, StatusDescription = x.Message };
            }
     
        }

        public CoolNameResponse GetCoolNames()
        {
            Task<NamesSaResponse> getNamesTask = _nameSa.GetNames();
            CoolNameResponse coolRes = new();
            getNamesTask.Wait();
            if(getNamesTask.Result.StatusCode != StatusCodes.Status200OK)
            {
                coolRes.StatusCode = getNamesTask.Result.StatusCode;
                coolRes.StatusDescription = getNamesTask.Result.StatusDescription;
                return coolRes;
            }
            Name[] names = getNamesTask.Result.NamesArray;

            if(names != null && names.Length >0)
            {
                coolRes.StatusCode = getNamesTask.Result.StatusCode;
                coolRes.StatusDescription = getNamesTask.Result.StatusDescription;
                coolRes.CoolNames = PopulateCoolNames(names).ToArray();
            }
            return coolRes;
        }

        public async Task<Response> PutNameAsync(int id, PutNameRequest name)
        {
            SAPutNameRequest saReq = name.ConvertToSaPutNameRequest(id);
            return await _nameSa.PutNameAsync(saReq);
        }

        public async Task<Response> PatchNameAsync(int id, JsonPatchDocument patch)
        {
            try
            {
                if (id < 0) throw new ArgumentNullException("id");
                if (patch == null) throw new ArgumentNullException("patch");
                return await _nameSa.PatchNameAsync(id, patch);
            }
            catch (Exception x)
            {
                return new Response() { StatusCode = (int)StatusCodes.Status400BadRequest, StatusDescription = x.Message };
            }

        }

        public async Task<Response> DeleteNameAsync(int id)
        {
            try
            {
                if (id < 0) throw new ArgumentNullException("id");
                return await _nameSa.DeleteNameAsync(id);
            }
            catch (Exception x)
            {
                return new Response() { StatusCode = (int)StatusCodes.Status400BadRequest, StatusDescription = x.Message };
            }

        }
        private IEnumerable<CoolName>? PopulateCoolNames(Name[]? names)
        {
            if(names == null || names.Length == 0) { yield return null; };

            for(var i=0; i<names.Length; i++)
            {
                yield return new CoolName(names[i].Id, names[i].NickName, names[i].FirstName, names[i].LastName, names[i].PersonId);

            }
        }


    }
}
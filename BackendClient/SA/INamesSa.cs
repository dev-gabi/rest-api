using Entities.NamesSa;
using Entities.Request;
using Entities.Response;
using Microsoft.AspNetCore.JsonPatch;

namespace SA
{
    public interface INamesSa
    {

        public Task<NamesSaResponse>  GetNames();
        Task<Response> PostNameAsync(PostNameRequest name);
        Task<Response> PutNameAsync(SAPutNameRequest nameReq);
        Task<Response> DeleteNameAsync(int id);
        Task<Response> PatchNameAsync(int id, JsonPatchDocument patch);
    }
}

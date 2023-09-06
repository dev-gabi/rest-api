using Entities.Request;
using Entities.Response;
using Microsoft.AspNetCore.JsonPatch;

namespace BL
{
    public interface INameService
    {
        Response AddName(AddNameReq name);
        NamesResponse GetNamesWithPersonId();
        Response UpdateName(UpdateNameReq name);
        public Response DeleteName(int id);
        Response UpdatePatchName(int id, JsonPatchDocument name);
    }
}

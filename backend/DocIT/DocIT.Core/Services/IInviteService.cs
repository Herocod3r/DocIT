using System;
using DocIT.Core.Data.Models;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.ViewModels;
using System.Threading.Tasks;

namespace DocIT.Core.Services
{
    public interface IInviteService
    {
        Task<ListViewModel<InviteViewModel>> GetUserInvites(string email, int skip, int limit);
        Task<InviteViewModel> CreateInvite(InvitePayload payload,Guid userId);
        Task DeleteInvite(DeleteInvitePayload payload, Guid userId);
    }
}

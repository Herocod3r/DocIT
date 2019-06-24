using System;
using System.Threading.Tasks;
using DocIT.Core.Data.Models;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.ViewModels;
using DocIT.Core.Repositories;
using DocIT.Core.Services.Exceptions;
using System.Linq;

namespace DocIT.Core.Services.Implementations
{
    public class InviteService : IInviteService
    {
        private readonly IProjectInviteRepository repository;

        public InviteService(IProjectInviteRepository repository)
        {
            this.repository = repository;
        }

        public async Task<InviteViewModel> CreateInvite(InvitePayload payload, Guid userId)
        {
            try
            {
                var rsp = await Task.Run(()=> repository.CreateInvite(new Invite { Email = payload.Email }, payload.ProjectId,userId));
                return FromInviteItem(rsp);
            }
            catch (ArgumentException ex)
            {
                throw new InviteException(ex.Message, 0);
            }
            catch(InvalidOperationException ex)
            {
                throw new InviteException(ex.Message, 1);
            }
        }

        public async Task DeleteInvite(DeleteInvitePayload payload, Guid userId)
        {
            try
            {
                 await Task.Run(() => repository.DeleteInvite(new Invite { Email = payload.Email }, payload.ProjectId,userId));
                
            }
            catch (ArgumentException ex)
            {
                throw new InviteException(ex.Message, 0);
            }
            catch (InvalidOperationException ex)
            {
                throw new InviteException(ex.Message, 1);
            }
        }

        public async Task<ListViewModel<InviteViewModel>> GetUserInvites(string email, int skip, int limit)
        {
            limit = Math.Min(limit, 30);
            var (itms, count) = await Task.Run(() => repository.GetUserInvites(email, skip, limit));
            return new ListViewModel<InviteViewModel>
            {
                Result = itms.Select(x => FromInviteItem(x)).ToList(),
                Total = count
            };
        }

        private InviteViewModel FromInviteItem(InviteItem item) => new InviteViewModel { Email = item.Invites.FirstOrDefault()?.Email, Inviter = item.InviterName, ProjectId = item.ProjectName, ProjectName = item.ProjectName };
    }
}

using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ForumService : IForumService
    {
        private readonly SpeditorDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ForumService(SpeditorDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
            this._mapper = mapper;
        }

        public IEnumerable<Forum> GetAllForumThemes()
        {
            return this._context.Forums
                .Include(x => x.Posts);
        }

        public ForumViewModel GetForumThemeById(string id)
        {
            var forum = this._context.Forums.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<ForumViewModel>(forum);
        }

        public async Task<Forum> CreateForumTheme(ForumDTO forum)
        {
            var mappedForum = _mapper.Map<Forum>(forum);
            var result = this._context.Forums.Add(mappedForum);
            await this._context.SaveChangesAsync();
            return result.Entity;
        }
    }
}

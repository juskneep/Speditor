using Backend.DTOs;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IForumService
    {
        IEnumerable<Forum> GetAllForumThemes();

        ForumViewModel GetForumThemeById(string id);

        Task<Forum> CreateForumTheme(ForumDTO forum);
    }
}

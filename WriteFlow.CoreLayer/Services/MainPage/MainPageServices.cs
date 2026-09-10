using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.DTOs.MainPageDto;
using WriteFlow.CoreLayer.Mappers;
using WriteFlow.DataLayer.Context;

namespace WriteFlow.CoreLayer.Services.MainPage
{
    public class MainPageServices : IMainPageServices
    {
        private readonly WriteFlowContext _context;

        public MainPageServices(WriteFlowContext context)
        {
            _context = context;
        }

        public MainPageDto GetData()
        {
            var categories = _context.Categories
                .OrderByDescending(d => d.Id)
                .Take(6)
                .Include(c => c.Posts)
                .Include(c => c.SubPosts)
                .Select(category => new MainPageCategoryDto()
                {
                    Title = category.Title,
                    Slug = category.Slug,
                    PostChild = category.Posts.Count + category.SubPosts.Count,
                    IsMainCategory = category.ParentId == null
                }).ToList();

            var specialPosts = _context.Posts
                .OrderByDescending(d => d.Id)
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .Where(r => r.IsSpecial).Take(4).Select(post => PostMapper.MapPostToDto(post)).ToList();

            var latestPostFilter = _context.Posts
                .Include(c=>c.User)
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .OrderByDescending(d => d.Id)
                .Take(6).Select(post => PostMapper.MapPostToDto(post)).ToList();

            return new MainPageDto()
            {
                LastPosts = latestPostFilter,
                Categories = categories,
                SpecialPosts = specialPosts
            };
        }

        public SiteStatsDto GetSiteStats()
        {
            return new SiteStatsDto()
            {
                UserCount = _context.Users.Count(),
                PostCount = _context.Posts.Count(),
                CategoryCount = _context.Categories.Count()
            };
        }
    }
}

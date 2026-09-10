using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteFlow.CoreLayer.Utilities
{
    public class BasePagination
    {
        public int EntityCount { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageCount { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int Take { get; private set; }

        public int StartItem => EntityCount == 0 ? 0 : ((CurrentPage - 1) * Take) + 1;
        public int EndItem => Math.Min(CurrentPage * Take, EntityCount);

        public void GeneratePaging(IQueryable<Object> data, int take, int currentPage)
        {
            var entityCount = data.Count();
            var pageCount = (int)Math.Ceiling(entityCount / (double)take);

            EntityCount = entityCount;
            PageCount = pageCount;
            CurrentPage = currentPage;
            Take = take;

            if (pageCount <= 3)
            {
                StartPage = 1;
                EndPage = pageCount;
            }
            else if (currentPage <= 2)
            {

                StartPage = 1;
                EndPage = 3;
            }
            else if (currentPage + 1 >= pageCount)
            {
                StartPage = pageCount - 2;
                EndPage = pageCount;
            }
            else
            {
                StartPage = currentPage - 1;
                EndPage = currentPage + 1;
            }
        }
    }
}

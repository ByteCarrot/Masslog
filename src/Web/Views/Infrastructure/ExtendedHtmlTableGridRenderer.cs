using MvcContrib.Pagination;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ByteCarrot.Masslog.Web.Views.Infrastructure
{
    public class ExtendedHtmlTableGridRenderer<T> : HtmlTableGridRenderer<T> where T : class
    {
        private readonly string _container;

        public ExtendedHtmlTableGridRenderer(string container)
        {
            _container = container;
        }

        protected override void RenderHeaderText(GridColumn<T> column)
        {
            if (IsSortingEnabled && column.Sortable)
            {
                var sortColumnName = GenerateSortColumnName(column);
                var flag = GridModel.SortOptions.Column == sortColumnName;
                var sortOptions = new GridSortOptions { Column = sortColumnName };
                if (flag)
                {
                    sortOptions.Direction = GridModel.SortOptions.Direction == SortDirection.Ascending
                        ? SortDirection.Descending
                        : SortDirection.Ascending;
                }
                else
                {
                    var gridSortOptions = sortOptions;
                    var initialDirection = column.InitialDirection;
                    var num = initialDirection.HasValue ? (int)initialDirection.GetValueOrDefault() : (int)GridModel.SortOptions.Direction;
                    gridSortOptions.Direction = (SortDirection)num;
                }
                var valuesForSortOptions = CreateRouteValuesForSortOptions(sortOptions, GridModel.SortPrefix);
                foreach (var key in Context.RequestContext.HttpContext.Request.QueryString.AllKeys.Where(key => key != null))
                {
                    if (!valuesForSortOptions.ContainsKey(key))
                    {
                        valuesForSortOptions[key] = Context.RequestContext.HttpContext.Request.QueryString[key];
                    }
                }

                RenderHeader(column, UrlHelper.GenerateUrl(null, "sort", null, valuesForSortOptions, RouteTable.Routes, Context.RequestContext, false));
            }
            else base.RenderHeaderText(column);
        }

        private void RenderHeader(GridColumn<T> column, string url)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", "#");
            a.MergeAttribute("data-jo", "{\"action\":\"get\",\"url\":\"" + url + "\",\"target\":\"" + _container + "\"}");
            a.SetInnerText(column.DisplayName);
            RenderText(a.ToString(TagRenderMode.Normal));
        }

        private static RouteValueDictionary CreateRouteValuesForSortOptions(GridSortOptions sortOptions, string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return new RouteValueDictionary(sortOptions);
            }
            
            return new RouteValueDictionary(new Dictionary<string, object>
            { 
                { prefix + ".Column", sortOptions.Column },
                { prefix + ".Direction", sortOptions.Direction }
            });
        }

        protected override void RenderGridEnd(bool isEmpty)
        {
            base.RenderGridEnd(isEmpty);
            var p = (IPagination) DataSource;
            if (p.TotalPages > 1)
            {
                RenderText(new ExtendedPager(_container, p, Context).ToHtmlString());
            }
        }
    }
}
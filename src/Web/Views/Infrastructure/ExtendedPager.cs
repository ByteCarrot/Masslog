using MvcContrib.Pagination;
using MvcContrib.UI.Pager;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace ByteCarrot.Masslog.Web.Views.Infrastructure
{
    public class ExtendedPager : Pager
    {
        private readonly string _container;
        private readonly IPagination _pagination;

        public ExtendedPager(string container, IPagination pagination, ViewContext context)
            : base(pagination, context)
        {
            _container = container;
            _pagination = pagination;
        }

        private string GetUrl(int page)
        {
            return UrlHelper.GenerateUrl(null, "page", null, new RouteValueDictionary { { "page", page } },
                                         RouteTable.Routes, ViewContext.RequestContext, true);
        }

        protected override void RenderLeftSideOfPager(StringBuilder builder)
        {
        }

        protected override void RenderRightSideOfPager(StringBuilder builder)
        {
            builder.Append("<div class=\"btn-group pagination\">");

            AppendButton(builder, "First", _pagination.PageNumber > 1, 1);
            AppendButton(builder, "Previous", _pagination.HasPreviousPage, _pagination.PageNumber - 1);

            builder.AppendFormat("<button class=\"btn disabled span8\">Showing {0} - {1} of {2}</button>",
                _pagination.FirstItem, _pagination.LastItem, _pagination.TotalItems);

            AppendButton(builder, "Next", _pagination.HasNextPage, _pagination.PageNumber + 1);
            AppendButton(builder, "Last", _pagination.PageNumber < _pagination.TotalPages, _pagination.TotalPages);

            builder.Append("</div>");
        }

        private void AppendButton(StringBuilder sb, string text, bool enabled, int page)
        {
            var b = new TagBuilder("button");
            b.AddCssClass("span1");
            b.AddCssClass("btn");
            if (enabled)
            {
                b.MergeAttribute("data-jo", "{\"action\":\"get\",\"url\":\"" + GetUrl(page) + "\",\"target\":\"" + _container + "\"}");
            } 
            else
            {
                b.MergeAttribute("disabled", "disabled");
            }
            b.SetInnerText(text);

            sb.Append(b.ToString(TagRenderMode.Normal));
        }
    }
}
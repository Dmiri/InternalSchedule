﻿using Hnatob.WebUI.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Hnatob.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PagingInfo pagingInfo,
            Func<int, string> pageUrl)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (pagingInfo.TotalPages > 1)
            {
                TagBuilder buttonGroup = new TagBuilder("div");
                buttonGroup.AddCssClass("btn-group");
                buttonGroup.AddCssClass("float-right");
                for (int i = 1; i <= pagingInfo.TotalPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href", pageUrl(i));
                    tag.InnerHtml = i.ToString();
                    if (i == pagingInfo.CurrentPage)
                    {
                        tag.AddCssClass("selected");
                        tag.AddCssClass("btn btn-primary");
                    }
                    else tag.AddCssClass("btn btn-secondary");
                    stringBuilder.Append(tag.ToString());
                }
                buttonGroup.InnerHtml = stringBuilder.ToString();
                stringBuilder.Clear();
                stringBuilder.Append(buttonGroup.ToString());
            }
            return MvcHtmlString.Create(stringBuilder.ToString());
        }


        public static MvcHtmlString PageLinksForSchedule(this HtmlHelper html,
            PagingInfo pagingInfo,
            Func<int, string> pageUrl)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (pagingInfo.TotalPages > 1)
            {
                TagBuilder buttonGroup = new TagBuilder("div");
                buttonGroup.AddCssClass("btn-group");
                buttonGroup.AddCssClass("float-right");
                StringBuilder buttonBuilder = new StringBuilder();
                //List<TagBuilder> buttonList = new List<TagBuilder>();

                // Add button
                //---------------------------------------------------

                if (pagingInfo.CurrentPage > 1 
                    && pagingInfo.CurrentPage -1 != pagingInfo.DefaulpPage)
                {
                    TagBuilder buttonFirst = new TagBuilder("a");
                    buttonFirst.MergeAttribute("href", pageUrl(1));
                    buttonFirst.InnerHtml = "First";
                    if (pagingInfo.CurrentPage == 1)
                    {
                        buttonFirst.AddCssClass("selected");
                        buttonFirst.AddCssClass("btn btn-primary");
                    }
                    else buttonFirst.AddCssClass("btn btn-secondary");
                    buttonBuilder.Append(buttonFirst.ToString());
                }

                if (pagingInfo.CurrentPage > 2)
                {
                    TagBuilder buttonPrevious = new TagBuilder("a");
                    buttonPrevious.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
                    buttonPrevious.InnerHtml = "Previous";
                    buttonPrevious.AddCssClass("btn btn-secondary");
                    buttonBuilder.Append(buttonPrevious.ToString());
                }

                if (pagingInfo.CurrentPage < pagingInfo.DefaulpPage && pagingInfo.CurrentPage != 1)
                {
                    TagBuilder buttonThisPrevious = new TagBuilder("a");
                    buttonThisPrevious.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage));
                    buttonThisPrevious.InnerHtml = "This";
                    if (pagingInfo.CurrentPage != pagingInfo.DefaulpPage)
                    {
                        buttonThisPrevious.AddCssClass("selected");
                        buttonThisPrevious.AddCssClass("btn btn-primary");
                    }
                    else buttonThisPrevious.AddCssClass("btn btn-secondary");
                    buttonBuilder.Append(buttonThisPrevious.ToString());
                }

                TagBuilder buttonNow = new TagBuilder("a");
                buttonNow.MergeAttribute("href", pageUrl(pagingInfo.DefaulpPage));
                buttonNow.InnerHtml = "Now";
                if (pagingInfo.CurrentPage == pagingInfo.DefaulpPage)
                {
                    buttonNow.AddCssClass("selected");
                    buttonNow.AddCssClass("btn btn-primary");
                }
                else buttonNow.AddCssClass("btn btn-secondary");
                buttonBuilder.Append(buttonNow.ToString());


                if (pagingInfo.CurrentPage > pagingInfo.DefaulpPage && pagingInfo.CurrentPage != pagingInfo.TotalPages)
                {
                    TagBuilder buttonThisNext = new TagBuilder("a");
                    buttonThisNext.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage));
                    buttonThisNext.InnerHtml = "This";
                    if (pagingInfo.CurrentPage != pagingInfo.DefaulpPage)
                    {
                        buttonThisNext.AddCssClass("selected");
                        buttonThisNext.AddCssClass("btn btn-primary");
                    }
                    else buttonThisNext.AddCssClass("btn btn-secondary");
                    buttonBuilder.Append(buttonThisNext.ToString());
                }

                if (pagingInfo.TotalPages - pagingInfo.CurrentPage > 1)
                {
                    TagBuilder buttonNext = new TagBuilder("a");
                    buttonNext.MergeAttribute("href", pageUrl(1));
                    buttonNext.InnerHtml = "Next";
                    buttonNext.AddCssClass("btn btn-secondary");
                    buttonBuilder.Append(buttonNext.ToString());
                }

                if (pagingInfo.TotalPages - pagingInfo.CurrentPage >= 1
                    || pagingInfo.CurrentPage == pagingInfo.TotalPages
                    && pagingInfo.CurrentPage != pagingInfo.DefaulpPage)
                {
                    TagBuilder buttonLast = new TagBuilder("a");
                    buttonLast.MergeAttribute("href", pageUrl(pagingInfo.TotalPages));
                    buttonLast.InnerHtml = "Last";
                    if (pagingInfo.CurrentPage == pagingInfo.TotalPages)
                    {
                        buttonLast.AddCssClass("selected");
                        buttonLast.AddCssClass("btn btn-primary");
                    }
                    else buttonLast.AddCssClass("btn btn-secondary");
                    buttonBuilder.Append(buttonLast.ToString());
                }

                buttonGroup.InnerHtml = buttonBuilder.ToString();
                stringBuilder.Append(buttonGroup.ToString());

            }


            return MvcHtmlString.Create(stringBuilder.ToString());
        }
    }
}
#pragma checksum "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Projections.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "87b0b1994c400e63623573962a47f08983cd3dec"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BuyTicket_Projections), @"mvc.1.0.view", @"/Views/BuyTicket/Projections.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/BuyTicket/Projections.cshtml", typeof(AspNetCore.Views_BuyTicket_Projections))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 2 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\_ViewImports.cshtml"
using AlphaCinema;

#line default
#line hidden
#line 3 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\_ViewImports.cshtml"
using AlphaCinema.Models;

#line default
#line hidden
#line 4 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\_ViewImports.cshtml"
using AlphaCinema.Models.AccountViewModels;

#line default
#line hidden
#line 5 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\_ViewImports.cshtml"
using AlphaCinema.Models.ManageViewModels;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"87b0b1994c400e63623573962a47f08983cd3dec", @"/Views/BuyTicket/Projections.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f7a067fd21f8a0ba7a895f7a75510b6444d4761d", @"/Views/_ViewImports.cshtml")]
    public class Views_BuyTicket_Projections : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AlphaCinemaWeb.Models.ProjectionModels.ProjectionListViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "BuyTicket", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Detail", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(71, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Projections.cshtml"
  
    ViewData["Title"] = "Projections";

#line default
#line hidden
            BeginContext(120, 670, true);
            WriteLiteral(@"
<div id=""assets"">
    <h3>Movie Catalog</h3>
    <div id=""assetsTable"">
        <table class=""table table-condensed"" id=""catalogIndexTable"">
            <thead>
                <!--The <thread> tag is used to group header content in an HTML table.
                   The <thread> element is used in conjunction with the <tbody> and
                <tfoot> elements to specify each part of a table (header, body, footer).-->
                <tr>
                    <th>Image</th>
                    <th>Title</th>
                    <th>Genres</th>
                    <th>Duration</th>
                </tr>
            </thead>
            <tbody>
");
            EndContext();
#line 23 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Projections.cshtml"
                 foreach (var movie in Model.Projections)
                {

#line default
#line hidden
            BeginContext(868, 101, true);
            WriteLiteral("                    <tr class=\"assetRow\">\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(969, 221, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0631bbee498c4b5c9beb77451d72b597", async() => {
                BeginContext(1049, 38, true);
                WriteLiteral("\r\n                                <img");
                EndContext();
                BeginWriteAttribute("src", " src=\"", 1087, "\"", 1134, 1);
#line 28 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Projections.cshtml"
WriteAttributeValue("", 1093, movie.ImageUrl??"~/images/default.jpg", 1093, 41, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1135, 51, true);
                WriteLiteral(" class=\"imageCell\" />\r\n                            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-projection", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 27 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Projections.cshtml"
                                                                                        WriteLiteral(movie);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["projection"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-projection", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["projection"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1190, 61, true);
            WriteLiteral("\r\n                        </td>\r\n                        <td>");
            EndContext();
            BeginContext(1252, 15, false);
#line 31 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Projections.cshtml"
                       Write(movie.MovieName);

#line default
#line hidden
            EndContext();
            BeginContext(1267, 37, true);
            WriteLiteral("</td>\r\n                        <td>\r\n");
            EndContext();
#line 33 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Projections.cshtml"
                             foreach (var genre in movie.Genres)
                            {
                                

#line default
#line hidden
            BeginContext(1434, 5, false);
#line 35 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Projections.cshtml"
                           Write(genre);

#line default
#line hidden
            EndContext();
#line 35 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Projections.cshtml"
                                      
                            }

#line default
#line hidden
            BeginContext(1472, 59, true);
            WriteLiteral("                        </td>\r\n                        <td>");
            EndContext();
            BeginContext(1532, 19, false);
#line 38 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Projections.cshtml"
                       Write(movie.MovieDuration);

#line default
#line hidden
            EndContext();
            BeginContext(1551, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(1587, 16, false);
#line 39 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Projections.cshtml"
                       Write(movie.MovieStart);

#line default
#line hidden
            EndContext();
            BeginContext(1603, 34, true);
            WriteLiteral("</td>\r\n                    </tr>\r\n");
            EndContext();
#line 41 "D:\Programming\Projects\C#\Telerik\Teamworks\ASP.NET Core MVC\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Projections.cshtml"
                }

#line default
#line hidden
            BeginContext(1656, 38, true);
            WriteLiteral("        </table>\r\n    </div>\r\n</div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AlphaCinemaWeb.Models.ProjectionModels.ProjectionListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591

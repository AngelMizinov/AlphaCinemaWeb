#pragma checksum "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "17cc245dc0d7d34abac91534a914a2ff68374b58"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BuyTicket_Index), @"mvc.1.0.view", @"/Views/BuyTicket/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/BuyTicket/Index.cshtml", typeof(AspNetCore.Views_BuyTicket_Index))]
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
#line 1 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 2 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\_ViewImports.cshtml"
using AlphaCinema;

#line default
#line hidden
#line 3 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\_ViewImports.cshtml"
using AlphaCinemaWeb;

#line default
#line hidden
#line 4 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\_ViewImports.cshtml"
using AlphaCinemaWeb.Models.CityModels;

#line default
#line hidden
#line 5 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\_ViewImports.cshtml"
using AlphaCinemaWeb.Models.ProjectionModels;

#line default
#line hidden
#line 6 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\_ViewImports.cshtml"
using AlphaCinema.Models;

#line default
#line hidden
#line 7 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\_ViewImports.cshtml"
using AlphaCinema.Models.AccountViewModels;

#line default
#line hidden
#line 8 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\_ViewImports.cshtml"
using AlphaCinema.Models.ManageViewModels;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"17cc245dc0d7d34abac91534a914a2ff68374b58", @"/Views/BuyTicket/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bb3c345fb1dcf79ad8c4913fb50d70943e0df87b", @"/Views/_ViewImports.cshtml")]
    public class Views_BuyTicket_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CityListViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("d-block w-100"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/cinema1.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("First slide"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/cinema2.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Second slide"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/cinema3.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Third slide"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("dropdown-item"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "BuyTicket", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Movie", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/defaultMovie.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card-img-top"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("narrower"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            BeginContext(67, 203, true);
            WriteLiteral("\r\n<div id=\"carouselExampleFade\" class=\"carousel slide carousel-fade\" data-ride=\"carousel\" data-interval=\"3000\">\r\n    <div class=\"carousel-inner\">\r\n        <div class=\"carousel-item active\">\r\n            ");
            EndContext();
            BeginContext(270, 72, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "66695ded4d854e05a78bbd7b11582540", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(342, 67, true);
            WriteLiteral("\r\n        </div>\r\n        <div class=\"carousel-item\">\r\n            ");
            EndContext();
            BeginContext(409, 73, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "0749945724874849ae5d52bcdecfdf16", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(482, 67, true);
            WriteLiteral("\r\n        </div>\r\n        <div class=\"carousel-item\">\r\n            ");
            EndContext();
            BeginContext(549, 72, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "fd98f8d3634a4fa095c462197fb390b4", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(621, 970, true);
            WriteLiteral(@"
        </div>
    </div>
    <a class=""carousel-control-prev"" href=""#carouselExampleFade"" role=""button"" data-slide=""prev"">
        <span class=""carousel-control-prev-icon"" aria-hidden=""true""></span>
        <span class=""sr-only"">Previous</span>
    </a>
    <a class=""carousel-control-next"" href=""#carouselExampleFade"" role=""button"" data-slide=""next"">
        <span class=""carousel-control-next-icon"" aria-hidden=""true""></span>
        <span class=""sr-only"">Next</span>
    </a>
</div>

<div>
    <div class=""btn-group"" role=""group"" aria-label=""Button group with nested dropdown"">
        <div class=""btn-group"" role=""group"" style=""padding: 30px"">
            <button id=""btnGroupDrop1"" type=""button"" class=""btn btn-info dropdown-toggle"" data-toggle=""dropdown""
                    aria-haspopup=""true"" aria-expanded=""false"">
                Choose Town
            </button>
            <div class=""dropdown-menu"" aria-labelledby=""btnGroupDrop1"">
");
            EndContext();
#line 36 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml"
                 foreach (var city in Model.Cities)
                {

#line default
#line hidden
            BeginContext(1663, 20, true);
            WriteLiteral("                    ");
            EndContext();
            BeginContext(1683, 113, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c057e250192a42cd8e862d6c5975a29b", async() => {
                BeginContext(1783, 9, false);
#line 38 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml"
                                                                                                                  Write(city.Name);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-cityId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 38 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml"
                                                                                                 WriteLiteral(city.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["cityId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-cityId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["cityId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1796, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 39 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml"
                }

#line default
#line hidden
            BeginContext(1817, 206, true);
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n    <p1 style=\"font-size: 30px; padding: 10px 40px\">Our week Specials</p1>\r\n</div>\r\n\r\n<!-- Grid row -->\r\n<div class=\"row\" style=\"justify-content: flex-end\">\r\n");
            EndContext();
#line 48 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml"
     foreach (var movie in Model.ListOfMovies.Projections)
    {

#line default
#line hidden
            BeginContext(2090, 242, true);
            WriteLiteral("        <!-- Grid column -->\r\n        <div class=\"col-lg-3\">\r\n            <!--Card Narrower-->\r\n            <div class=\"card card-cascade narrower\">\r\n                <!--Card image-->\r\n                <div class=\"view view-cascade overlay\">\r\n");
            EndContext();
#line 56 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml"
                     if (movie.ImageUrl == null)
                    {

#line default
#line hidden
            BeginContext(2405, 24, true);
            WriteLiteral("                        ");
            EndContext();
            BeginContext(2429, 103, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "5d545d4bf80d46bab0b5efe35807516f", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2532, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 60 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml"
                    }
                    else
                    {

#line default
#line hidden
            BeginContext(2606, 118, true);
            WriteLiteral("                        <img src=\"movie.ImageUrl\" class=\"card-img-top\"\r\n                             alt=\"narrower\">\r\n");
            EndContext();
#line 65 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml"
                    }

#line default
#line hidden
            BeginContext(2747, 347, true);
            WriteLiteral(@"                    <a>
                        <div class=""mask rgba-white-slight""></div>
                    </a>
                </div>
                <!--/.Card image-->
                <!--Card content-->
                <div class=""card-body card-body-cascade"">
                    <h5 class=""pink-text""><i class=""fa fa-cutlery""></i>");
            EndContext();
            BeginContext(3095, 38, false);
#line 73 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml"
                                                                  Write(string.Join(" ", movie.Genres.Take(2)));

#line default
#line hidden
            EndContext();
            BeginContext(3133, 84, true);
            WriteLiteral("</h5>\r\n                    <!--Title-->\r\n                    <h4 class=\"card-title\">");
            EndContext();
            BeginContext(3218, 15, false);
#line 75 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml"
                                      Write(movie.MovieName);

#line default
#line hidden
            EndContext();
            BeginContext(3233, 107, true);
            WriteLiteral("</h4>\r\n                    <!--Text-->\r\n                    <p class=\"card-text\">\r\n                        ");
            EndContext();
            BeginContext(3341, 81, false);
#line 78 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml"
                   Write(movie.MovieDescription.Substring(0, Math.Min(movie.MovieDescription.Length, 200)));

#line default
#line hidden
            EndContext();
            BeginContext(3422, 31, true);
            WriteLiteral("...\r\n                    </p>\r\n");
            EndContext();
            BeginContext(3575, 165, true);
            WriteLiteral("                </div>\r\n                <!--/.Card content-->\r\n            </div>\r\n            <!--/.Card Narrower-->\r\n        </div>\r\n        <!-- Grid column -->\r\n");
            EndContext();
#line 88 "D:\Files\C# TeamProjects\AlphaCinemaWeb\AlphaCinemaWeb\AlphaCinemaWeb\Views\BuyTicket\Index.cshtml"
    }

#line default
#line hidden
            BeginContext(3747, 25, true);
            WriteLiteral("</div>\r\n<!-- Grid row -->");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CityListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
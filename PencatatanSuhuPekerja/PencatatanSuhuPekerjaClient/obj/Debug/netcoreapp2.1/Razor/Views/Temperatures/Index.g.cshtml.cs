#pragma checksum "C:\Users\Geraldy\source\repos\Kelompok2_Pencatatan-suhu-pekerja_Rio_Oscar\PencatatanSuhuPekerja\PencatatanSuhuPekerjaClient\Views\Temperatures\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2ff5d64a914ad383d00878cd77fc971af8d300c8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Temperatures_Index), @"mvc.1.0.view", @"/Views/Temperatures/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Temperatures/Index.cshtml", typeof(AspNetCore.Views_Temperatures_Index))]
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
#line 1 "C:\Users\Geraldy\source\repos\Kelompok2_Pencatatan-suhu-pekerja_Rio_Oscar\PencatatanSuhuPekerja\PencatatanSuhuPekerjaClient\Views\_ViewImports.cshtml"
using PencatatanSuhuPekerjaClient;

#line default
#line hidden
#line 2 "C:\Users\Geraldy\source\repos\Kelompok2_Pencatatan-suhu-pekerja_Rio_Oscar\PencatatanSuhuPekerja\PencatatanSuhuPekerjaClient\Views\_ViewImports.cshtml"
using PencatatanSuhuPekerjaClient.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2ff5d64a914ad383d00878cd77fc971af8d300c8", @"/Views/Temperatures/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bb163669fb431cacca098fe301bfb523c674d3a5", @"/Views/_ViewImports.cshtml")]
    public class Views_Temperatures_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/scripts/CRUDScripts/Temperatures.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\Geraldy\source\repos\Kelompok2_Pencatatan-suhu-pekerja_Rio_Oscar\PencatatanSuhuPekerja\PencatatanSuhuPekerjaClient\Views\Temperatures\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(90, 3208, true);
            WriteLiteral(@"
<div class=""header bg-primary pb-6"">
    <div class=""container-fluid"">
        <div class=""header-body"">
            <div class=""row align-items-center py-4"">
                <div class=""col-lg-6 col-7"">
                    <h6 class=""h2 text-white d-inline-block mb-0"">Tables</h6>
                    <nav aria-label=""breadcrumb"" class=""d-none d-md-inline-block ml-md-4"">
                        <ol class=""breadcrumb breadcrumb-links breadcrumb-dark"">
                            <li class=""breadcrumb-item""><a href=""#""><i class=""fas fa-home""></i></a></li>
                            <li class=""breadcrumb-item""><a href=""#"">Tables</a></li>
                            <li class=""breadcrumb-item active"" aria-current=""page"">Tables</li>
                        </ol>
                    </nav>
                </div>

            </div>
        </div>
    </div>
</div>
<!-- Page content -->
<div class=""container-fluid mt--6"">
    <div class=""row"">
        <div class=""col"">
            <div class");
            WriteLiteral(@"=""card"">
                <!-- Card header -->
                <div class=""card-header border-0"">
                    <h3 class=""mb-0"">Temperature Table</h3>
                    <div data-toggle=""modal"" data-target=""#exampleModal"" onclick=""ClearScreen();"">
                        <button class=""btn btn-outline-success btn-circle"" data-toggle=""tooltip"" data-placement=""top"" data-animation=""false"" title=""Add"">
                            <i class=""fa fa-plus-circle""></i>
                        </button>
                    </div>
                </div>
                <!-- Light table -->
                <div class=""table-responsive"">
                    <table class=""table align-items-center table-flush"" id=""dataTable"">
                        <thead class=""thead-light"">
                            <tr>
                                <th scope=""col"" class=""sort"" data-sort=""name"">No</th>
                                <th scope=""col"" class=""sort"" data-sort=""budget"">First Name</th>
            ");
            WriteLiteral(@"                    <th scope=""col"" class=""sort"" data-sort=""budget"">Last Name</th>
                                <th scope=""col"" class=""sort"" data-sort=""status"">Temperature</th>
                                <th scope=""col"" class=""sort"" data-sort=""completion"">Actions</th>
                            </tr>
                        </thead>
                        <tbody class=""list""></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <!-- Dark table -->
    <div class=""modal fade"" id=""exampleModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog"" role=""document"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"" id=""exampleModalLabel"">Division</h5>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                        <span aria-hidden=""tru");
            WriteLiteral("e\">&times;</span>\r\n                    </button>\r\n                </div>\r\n                <div class=\"modal-body\">\r\n                    ");
            EndContext();
            BeginContext(3298, 773, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c583d7a357c641809c4f700426e04422", async() => {
                BeginContext(3304, 760, true);
                WriteLiteral(@"
                        <div class=""form-group"">
                            <input name=""Id"" class=""form-control"" type=""hidden""
                                   placeholder=""Department Id"" id=""Id"" />
                        </div>
                        <div class=""form-group"">
                            <input name=""Name"" class=""form-control"" type=""text""
                                   placeholder=""Division Name"" id=""Name"" />
                        </div>
                        <div class=""form-group form-group-default"">
                            <label>Employee</label>
                            <select class=""form-control"" id=""DepartOption"" name=""DepartOption""></select>
                        </div>
                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4071, 1979, true);
            WriteLiteral(@"
                </div>
                <div class=""modal-footer"">
                    <button type=""button"" id=""Insert"" onclick=""Save();"" data-dismiss=""modal"" class=""btn btn-success button button4"">Insert</button>
                    <button type=""button"" id=""Update"" onclick=""Update();"" data-dismiss=""modal"" class=""btn btn-success button button4"">Edit</button>
                    <button type=""button"" data-dismiss=""modal"" class=""btn btn-warning button button4"">Cancel</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Footer -->
    <footer class=""footer pt-0"">
        <div class=""row align-items-center justify-content-lg-between"">
            <div class=""col-lg-6"">
                <div class=""copyright text-center  text-lg-left  text-muted"">
                    &copy; 2020 <a href=""https://www.creative-tim.com"" class=""font-weight-bold ml-1"" target=""_blank"">Creative Tim</a>
                </div>
            </div>
            <div class=""col-lg-6"">
       ");
            WriteLiteral(@"         <ul class=""nav nav-footer justify-content-center justify-content-lg-end"">
                    <li class=""nav-item"">
                        <a href=""https://www.creative-tim.com"" class=""nav-link"" target=""_blank"">Creative Tim</a>
                    </li>
                    <li class=""nav-item"">
                        <a href=""https://www.creative-tim.com/presentation"" class=""nav-link"" target=""_blank"">About Us</a>
                    </li>
                    <li class=""nav-item"">
                        <a href=""http://blog.creative-tim.com"" class=""nav-link"" target=""_blank"">Blog</a>
                    </li>
                    <li class=""nav-item"">
                        <a href=""https://github.com/creativetimofficial/argon-dashboard/blob/master/LICENSE.md"" class=""nav-link"" target=""_blank"">MIT License</a>
                    </li>
                </ul>
            </div>
        </div>
    </footer>
</div>


");
            EndContext();
            DefineSection("script", async() => {
                BeginContext(6066, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(6072, 61, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "67e9a72bad454cf8a312700819323700", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(6133, 670, true);
                WriteLiteral(@"
    <script src=""https://cdn.datatables.net/buttons/1.6.2/js/dataTables.buttons.min.js""></script>
    <script src=""https://cdn.datatables.net/buttons/1.6.2/js/buttons.flash.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js""></script>
    <script src=""https://cdn.datatables.net/buttons/1.6.2/js/buttons.html5.min.js""></script>
    <script src=""https://cdn.datatables.net/buttons/1.6.2/js/buttons.print.min.js""></script>
");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591

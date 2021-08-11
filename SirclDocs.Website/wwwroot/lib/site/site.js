// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// TABLE OF CONTENT:
////////////////////

$$(function () {
    if ($(".toc").length > 0) {
        var target = $(".toc")[0];
        var output = "";
        var i = 0;
        $("H2, H3, H4").each(function () {
            var elem = $(this)[0];
            // Get or assign id:
            var id = elem.id;
            if (id === "" || id === undefined) {
                elem.id = "id-" + (i++) + "-" + new Date().getTime();
            }
            // Build on TOC:
            output += "<div class=\"toc-h toc-" + elem.tagName.toLowerCase() + "\" onclick-scrollintoview=\"#" + elem.id + "\">" + elem.innerText + "</div>";
        });
        target.innerHTML = output;
    }
});
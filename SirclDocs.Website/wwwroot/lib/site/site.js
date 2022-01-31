// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// EXAMPLES:
////////////

// Rerun button:
$(document).on("click", "#rerun-btn", function () { $("#demo").html($("#demo-template").html()); });

// Delay loadings:
sircl.addRequestHandler("beforeSend", function(req) {
    if (req.$initialTarget.closest(".loaddelay500").length > 0) {
        window.setTimeout(function (processor, req) { processor.next(req); }, 500, this, req);
    } else if (req.$initialTarget.closest(".loaddelay1000").length > 0) {
        window.setTimeout(function (processor, req) { processor.next(req); }, 1000, this, req);
   } else {
        this.next(req);
   }
});

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
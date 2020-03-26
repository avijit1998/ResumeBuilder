$(document).ready(function () {
    
    function convertHtmlToPdf(targetDiv)
    {
        console.log(targetDiv);
        var doc = new jsPDF();

        var elementHTML = targetDiv.html();
        var specialElementHandlers = {
            '#editor': function (element, renderer) {
                return true;
            }
        };
        doc.fromHTML(elementHTML, 15, 15, {
            'width': 170,
            'elementHandlers': specialElementHandlers
        });
        doc.save('resume.pdf');
    }

    $('.js-print-btn').on('click',function(e){
        e.preventDefault();
        convertHtmlToPdf($(this.parentElement.parentElement.parentElement.parentElement.children).eq(1));
    })

});
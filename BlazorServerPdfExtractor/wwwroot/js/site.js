function updateIframeSrc() {
    var pdfSrc = localStorage.getItem('pdfSrc');
    var url = `/reports/report_${pdfSrc}.html#zoom=150`;
    var iframe = document.querySelector('.pdf-frame');
    if (iframe) {
        iframe.src = url;
    }
}

window.savePdfSrc = (pdfSrc) => {
    localStorage.setItem('pdfSrc', pdfSrc);
}
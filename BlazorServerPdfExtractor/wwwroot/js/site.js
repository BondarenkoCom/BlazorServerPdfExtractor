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

window.saveJsonPath = (jsonPath) => {
    localStorage.setItem('jsonPath', jsonPath);
}

window.getJsonPath = () => {
    return localStorage.getItem('jsonPath');
}

window.saveFileName = (fileName) => {
    localStorage.setItem('fileName', fileName);
}

window.getFileName = () => {
    return localStorage.getItem('fileName');
}
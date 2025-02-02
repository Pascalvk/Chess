function getElementBoundingBox(elementId) {
    var element = document.getElementById(elementId);
    var rect = element.getBoundingClientRect();

    // Return de positionering ten opzichte van de pagina
    return {
        top: rect.top + window.scrollY,    // De verticale positie ten opzichte van de pagina
        left: rect.left + window.scrollX,  // De horizontale positie ten opzichte van de pagina
        width: rect.width,                 // De breedte van het element
        height: rect.height,                // De hoogte van het element
        scrollx: window.scrollX,
        scrolly: window.scrollY
    };
}

function pruebaPuntoNetStatic() {
    DotNet.invokeMethodAsync("BlazorPeliculas.Client", "getCurrentCount").then(result => {
        console.log('contenido desde javaScript' + result);
    })
}

function pruebaPuntoNetInstacia(dotnetHelper) {
    dotnetHelper.invokeMethodAsync("IncrementCount");
}
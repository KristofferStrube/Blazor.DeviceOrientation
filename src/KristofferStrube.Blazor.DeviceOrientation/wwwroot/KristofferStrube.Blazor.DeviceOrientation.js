export function addListenerForDeviceOrientation(objRef) {
    if (typeof DeviceMotionEvent.requestPermission === "function") {
        DeviceMotionEvent.requestPermission().then(response => {
            if (response == 'granted') {
                window.addEventListener('deviceorientation', (e) => deviceOrientationListener(e, objRef));
            }
        });
    }
    else {
        Promise.all([navigator.permissions.query({ name: "accelerometer" }),
        navigator.permissions.query({ name: "gyroscope" })])
            .then((results) => {
                if (results.every((result) => result.state === "granted")) {
                    window.addEventListener('deviceorientation', (e) => deviceOrientationListener(e, objRef));
                }
            });
    }
}

function deviceOrientationListener(e, objRef) {
    objRef.invokeMethod("InvokeOnDeviceOrientation", {alpha: e.alpha, beta: e.beta, gamma: e.gamma, absolute: e.absolute});
}
export function addListenerForDeviceOrientation(objRef) {
    if (typeof DeviceMotionEvent.requestPermission === "function") {
        DeviceMotionEvent.requestPermission().then(response => {
            if (response == 'granted') {
                window.addEventListener('deviceorientation', (e) => deviceOrientationListener(e, objRef));
            }
            else {
                center.innerHTML = "Acces denied";
            }
        });
    }
    else {
        Promise.all([navigator.permissions.query({ name: "accelerometer" }),
        navigator.permissions.query({ name: "gyroscope" })])
            .then((results) => {
                if (results.every((result) => result.state === "granted")) {
                    window.addEventListener('deviceorientation', (e) => deviceOrientationListener(e, objRef));
                } else {
                    center.innerHTML = "Acces denied";
                }
            });
    }
}

function deviceOrientationListener(e, objRef) {
    objRef.invokeMethod("InvokeOnDeviceOrientation", e);
}
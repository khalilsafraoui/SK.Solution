window.showToastr = function (type, message) {
    if (type == "success") {
        console.log(message);
        toastr.success(message);
        console.log("done and should work");
    }

    if (type == "error") {
        toastr.error(message);
    }
}




//Toastr messages
window.ToastrSuccess = (message) => {
    toastr.success(message);
};

window.ToastrWarning = (message) => {
    toastr.warning(message);
};

window.ToastrError = (message) => {
    toastr.error(message);
};

//SweetAlert messages
window.SwalSuccess = (message) => {
    Swal.fire(
        'successful action!',
        message,
        'success'
    )
};

window.SwalWarning = (message) => {
    Swal.fire(
        'Warning!',
        message,
        'warning'
    )
};

window.SwalError = (message) => {
    Swal.fire(
        'failed action!',
        message,
        'error'
    )
};
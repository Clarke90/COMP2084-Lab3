// use jquery to attach a confirmation popup to any elements with the class "confirmation"
$('.confirmation').on('click', function () {
    return confirm('Are you sure you want to delete this?');
});
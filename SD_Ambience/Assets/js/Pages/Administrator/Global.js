var Administrator = {
    ViewProfile: function (email) {
        location.href = "/User/Details?email=" + email;
    },
    Delete: function (userId) {
        location.href = "/User/Delete?userId=" + userId;
    }
};
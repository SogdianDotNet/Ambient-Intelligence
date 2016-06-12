var Teacher =
    {
        ViewProfile: function (userEmail) {
            location.href = "/Teacher/Details?email=" + userEmail;
        },
        Delete: function (email) {
            location.href = "/Teacher/Delete?email=" + email;
        }
    };
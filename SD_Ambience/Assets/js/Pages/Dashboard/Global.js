var Dashboard =
    {
        StartNewSession: function () {
            location.href = "/Session/StartSession";
        },
        ViewReports: function (userEmail) {
            location.href = "/Report/ListView?userEmail=" + userEmail;
        },
        ShowHome: function () {
            location.href = "/Dashboard/Menu";
        }
    };
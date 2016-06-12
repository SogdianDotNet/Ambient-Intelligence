var Session = {
    GetList: function (email) {
        location.href = "/Session/ListView?email=" + email;
    },
    StopSession: function (id) {
        location.href = "/Session/StopSession?id=" + id;
    },
    GetCourseOverview: function (id) {
        location.href = "/Session/Details?id=" + id;
    }
};
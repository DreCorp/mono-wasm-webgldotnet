window.addEventListener('resize', () => {
    var c = document.getElementsByTagName('canvas')[0];

    if (c !== undefined) {
        c.width = window.innerWidth;
        c.height = window.innerHeight;

        that.resize(c.width, c.height);
    }
});

let that = this;

var App = {
    init: function () {
        that.start = Module.mono_bind_static_method("[rooms] Program:Start");
        that.resize = Module.mono_bind_static_method("[rooms] Program:Resize");
        that.start();
    }
};

document.body.addEventListener("load", App.init);


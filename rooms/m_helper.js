window.onresize = () => {
    that.resize(window.innerWidth, window.innerHeight);
}

let that = this;

var App = {			


    init: function () {
        that.resize = Module.mono_bind_static_method("[rooms] Program:Resize");
        that.start = Module.mono_bind_static_method("[rooms] Program:Start");				
        that.start();
    }
};

document.body.addEventListener("load", App.init);
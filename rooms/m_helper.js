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
        that.start = Module.mono_bind_static_method("[rooms] Program:Main");
        that.resize = Module.mono_bind_static_method("[rooms] Program:Resize");
        that.update = Module.mono_bind_static_method("[rooms] Program:Update");
        that.start();
    }
};

document.body.addEventListener("load", App.init);

let keyspressed = {
    up: false,
    down: false,
    left: false,
    right: false,
};

window.addEventListener('keydown', (e) => {

    if (e.key === 'Tab' || e.key === 'Alt') {
        return;
    }
    if (e.repeat === true) {
        return;
    }
    if (e.key === 'w') { keyspressed.up = true; }
    if (e.key === 's') { keyspressed.down = true; }
    if (e.key === 'a') { keyspressed.left = true; }
    if (e.key === 'd') { keyspressed.right = true; }
});

window.addEventListener('keyup', (e) => {

    if (e.key === 'w') { keyspressed.up = false; }
    if (e.key === 's') { keyspressed.down = false; }
    if (e.key === 'a') { keyspressed.left = false; }
    if (e.key === 'd') { keyspressed.right = false; }
});

const FPS = 1000.0 / 30.0;// ms / frames doesnt work properly
let lastDrawTime = -1;// in ms
const initialTime = Date.now();
var currTime;
var elapsedTime;

function update_stuff() {
    //window.setInterval(update_stuff, 300);
    window.requestAnimationFrame(update_stuff);
    currTime = Date.now();

    if (currTime >= lastDrawTime + FPS) {

        lastDrawTime = currTime;

        elapsedTime = currTime - initialTime;
        //console.log(lastDrawTime);
        that.update(keyspressed);
    }
}
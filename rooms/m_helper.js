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
        that.resize = Module.mono_bind_static_method("[rooms] Program:ResizeViewport");
        that.update = Module.mono_bind_static_method("[rooms] Program:Update");
        that.change = Module.mono_bind_static_method("[rooms] Program:ChangeDrawPrimitive");
        that.add = Module.mono_bind_static_method("[rooms] Program:AddCube");
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

    if (e.key === 'p') { that.change(); }
    if (e.key === 'o') { that.add(); }

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

const FPS = 1000.0 / 10.0;// ms / frames doesnt work properly
let lastDrawTime = -1;// in ms
const initialTime = Date.now();
var currTime;
var elapsedTime;

function update_game() {

    window.requestAnimationFrame(update_game);

    currTime = Date.now();

    if (currTime >= lastDrawTime + FPS) {

        lastDrawTime = currTime;
        elapsedTime = currTime - initialTime;

        that.update(keyspressed);
    }
}
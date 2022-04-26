let that = this;

var App = {
    init: function () {
        that.start = Module.mono_bind_static_method("[mwwgdn] Program:Main");
        that.resize = Module.mono_bind_static_method("[mwwgdn] Program:ResizeViewport");
        that.update = Module.mono_bind_static_method("[mwwgdn] Program:Update");
        that.change = Module.mono_bind_static_method("[mwwgdn] Program:ChangeDrawPrimitive");
        that.add = Module.mono_bind_static_method("[mwwgdn] Program:AddCube");
        that.start();
    }
};

document.body.addEventListener("load", App.init);

window.addEventListener('resize', () => {
    var c = document.getElementsByTagName('canvas')[0];

    if (c !== undefined) {
        c.width = window.innerWidth;
        c.height = window.innerHeight;

        that.resize(c.width, c.height);
    }
});

let keyspressed = {
    forward: false,
    backward: false,
    left: false,
    right: false,
    up: false,
    down: false,
    rot_left: false,
    rot_right: false,

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

    if (e.key === 'w') { keyspressed.forward = true; }
    if (e.key === 's') { keyspressed.backward = true; }
    if (e.key === 'a') { keyspressed.left = true; }
    if (e.key === 'd') { keyspressed.right = true; }
    if (e.key === 'q') { keyspressed.up = true; }
    if (e.key === 'e') { keyspressed.down = true; }
    if (e.key === 'ArrowLeft') { keyspressed.rot_left = true; }
    if (e.key === 'ArrowRight') { keyspressed.rot_right = true; }
});

window.addEventListener('keyup', (e) => {

    if (e.key === 'w') { keyspressed.forward = false; }
    if (e.key === 's') { keyspressed.backward = false; }
    if (e.key === 'a') { keyspressed.left = false; }
    if (e.key === 'd') { keyspressed.right = false; }
    if (e.key === 'q') { keyspressed.up = false; }
    if (e.key === 'e') { keyspressed.down = false; }
    if (e.key === 'ArrowLeft') { keyspressed.rot_left = false; }
    if (e.key === 'ArrowRight') { keyspressed.rot_right = false; }
});

const FPS = 1000.0 / 30.0;// ms / frames doesnt work properly
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
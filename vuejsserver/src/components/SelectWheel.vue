<template>
<div class="selectwheel" style="z-index: 1; overflow-x: hidden; overflow-x: hidden; background-color:transparent; scrollbar-width: none;">
    <div class="wheel" ref="wheel" v-if="whichwheel == 1">
        <div class="arc"><i @click="setWheel(1,whichwheel)" class="fas fa-money" style="color:#3F6791;text-shadow: 0 0 2px #000;"></i></div>
        <div class="arc"><i @click="setWheel(2,whichwheel)" class="fas fa-key" style="color:#3F6791;text-shadow: 0 0 2px #000;"></i></div>
        <div class="arc"><i @click="setWheel(3,whichwheel)" class="fas fa-play-circle" style="color:#3F6791;text-shadow: 0 0 2px #000;"></i></div>
        <div class="arc"><i @click="setWheel(4,whichwheel)" class="fas fa-car" style="color:#3F6791;text-shadow: 0 0 2px #000;"></i></div>
        <div class="arc"><i @click="setWheel(5,whichwheel)" class="fas fa-id-card-alt" style="color:#3F6791;text-shadow: 0 0 2px #000;"></i></div>
        <div class="arc"><i @click="setWheel(6,whichwheel)" class="fa-solid fa-passport" style="color:#3F6791;text-shadow: 0 0 2px #000;"></i></div>
        <div class="arc"><i @click="setWheel(7,whichwheel)" class="fas fa-user" style="color:#3F6791;text-shadow: 0 0 2px #000;"></i></div>
        <div class="arc"><i @click="setWheel(8,whichwheel)" class="fa-solid fa-dog" style="color:#3F6791;text-shadow: 0 0 2px #000;"></i></div>
    </div>
    <div class="wheel" ref="wheel" v-if="whichwheel == 2">
        <div class="arc"><img @click="setWheel(1,whichwheel)" src="../assets/images/clothshop/kopfbedeckung.png"></div>
        <div class="arc"><img @click="setWheel(2,whichwheel)" src="../assets/images/clothshop/brillen.png"></div>
        <div class="arc"><img @click="setWheel(3,whichwheel)" src="../assets/images/clothshop/oberbekleidung.png"></div>
        <div class="arc"><img @click="setWheel(4,whichwheel)" src="../assets/images/clothshop/tshirt.png"></div>
        <div class="arc"><img @click="setWheel(5,whichwheel)" src="../assets/images/clothshop/schuh.png"></div>
        <div class="arc"><img @click="setWheel(6,whichwheel)" src="../assets/images/clothshop/hosen.png"></div>
        <div class="arc"><img @click="setWheel(7,whichwheel)" src="../assets/images/clothshop/accessoires.png"></div>
        <div class="arc"><img @click="setWheel(8,whichwheel)" src="../assets/images/clothshop/maske.png"></div>
    </div>
    <div class="wheel" ref="wheel" v-if="whichwheel == 3">
        <div class="arc"><img @click="setWheel(1,whichwheel)" src="../assets/images/cuffs.png"></div>
        <div class="arc"><img @click="setWheel(2,whichwheel)" src="../assets/images/grab.png"></div>
        <div class="arc"><img @click="setWheel(3,whichwheel)" src="../assets/images/search.png"></div>
        <div class="arc"><img @click="setWheel(4,whichwheel)" src="../assets/images/spike.png"></div>
        <div class="arc"><img @click="setWheel(5,whichwheel)" src="../assets/images/cone.png"></div>
        <div class="arc"><img @click="setWheel(6,whichwheel)" src="../assets/images/barrier1.png"></div>
        <div class="arc"><img @click="setWheel(7,whichwheel)" src="../assets/images/barrier2.png"></div>
        <div class="arc"><img @click="setWheel(8,whichwheel)" src="../assets/images/cell.png"></div>
    </div>
    <div class="wheel" ref="wheel" v-if="whichwheel == 4">
        <div class="arc"><img @click="setWheel(1,whichwheel)" src="../assets/images/inventory/Defribrilator.png"></div>
        <div class="arc"><img @click="setWheel(2,whichwheel)" src="../assets/images/inventory/Stethoskop.png"></div>
        <div class="arc"><img @click="setWheel(3,whichwheel)" src="../assets/images/recipe.png"></div>
        <div class="arc"><img @click="setWheel(4,whichwheel)" src="../assets/images/first-aid-kit.png"></div>
        <div class="arc"><img @click="setWheel(5,whichwheel)" src="../assets/images/cone.png"></div>
        <div class="arc"><img @click="setWheel(6,whichwheel)" src="../assets/images/barrier1.png"></div>
        <div class="arc"><img @click="setWheel(7,whichwheel)" src="../assets/images/barrier2.png"></div>
        <div class="arc"><img @click="setWheel(8,whichwheel)" src=""></div>
    </div>
    <div class="wheel" ref="wheel" v-if="whichwheel == 5">
        <div class="arc"><img @click="setWheel(5,whichwheel)" src="../assets/images/cone.png"></div>
        <div class="arc"><img @click="setWheel(6,whichwheel)" src="../assets/images/barrier1.png"></div>
        <div class="arc"><img @click="setWheel(7,whichwheel)" src="../assets/images/barrier2.png"></div>
        <div class="arc"><img @click="setWheel(8,whichwheel)" src="../assets/images/inventory/Parkkralle.png"></div>
        <div class="arc"><img @click="setWheel(25,whichwheel)" src=""></div>
        <div class="arc"><img @click="setWheel(25,whichwheel)" src=""></div>
        <div class="arc"><img @click="setWheel(25,whichwheel)" src=""></div>
        <div class="arc"><img @click="setWheel(25,whichwheel)" src=""></div>
    </div>
    <div class="wheel" ref="wheel" v-if="whichwheel == 6">
        <div class="arc"><img @click="setWheel(1,whichwheel)" src="../assets/images/cuffs.png"></div>
        <div class="arc"><img @click="setWheel(2,whichwheel)" src="../assets/images/grab.png"></div>
        <div class="arc"><img @click="setWheel(3,whichwheel)" src="../assets/images/search.png"></div>
        <div class="arc"><img @click="setWheel(25,whichwheel)" src=""></div>
        <div class="arc"><img @click="setWheel(25,whichwheel)" src=""></div>
        <div class="arc"><img @click="setWheel(25,whichwheel)" src=""></div>
        <div class="arc"><img @click="setWheel(25,whichwheel)" src=""></div>
        <div class="arc"><img @click="setWheel(25,whichwheel)" src=""></div>
    </div>
</div>
</template>

<script>
import Swal from 'sweetalert2/dist/sweetalert2.js'

//Selectwheel functions
document.body.addEventListener('contextmenu', e => e.preventDefault() & e.stopPropagation());
document.body.addEventListener('mousemove', onMouseMove);
document.body.addEventListener('touchmove', e => onMouseMove(e.touches[0]));

let showing, anchorX, anchorY, min = 100;
let wheel = null;

function onMouseMove({
    clientX: x,
    clientY: y
}) {
    if (!showing) return;
    let dx = x - anchorX;
    let dy = y - anchorY;
    let mag = Math.sqrt(dx * dx + dy * dy);
    let index = 0;
    if (mag >= min) {
        let deg = Math.atan2(dy, dx) + 0.625 * Math.PI;
        while (deg < 0) deg += Math.PI * 2;
        index = Math.floor(deg / Math.PI * 4) + 1;
    }
    wheel.setAttribute('data-chosen', index);
}

export default {
    name: 'SelectWheel',
    data: function () {
        return {
            dropvalue: 0,
            showswal: false,
            whichwheel: 1,
        }
    },
    mounted() {
        document.body.classList.add("dark-mode");
    },
    methods: {
        hideWheel: function () {
            if (!showing) return;
            showing = false;
            wheel.setAttribute('data-chosen', 0);
            wheel.classList.remove('on');
            this.whichwheel = 0;
        },
        showWheel: function (wheelid) {
            if (showing || this.showswal) return;
            showing = true;
            this.whichwheel = wheelid;
            var self = this;
            setTimeout(function () {
                wheel = self.$refs.wheel;
                wheel = document.querySelector('.wheel');
                window.selectionwheel = self;
                anchorX = window.innerWidth / 2;
                anchorY = window.innerHeight / 2;
                wheel.style.setProperty('--x', `${anchorX}px`);
                wheel.style.setProperty('--y', `${anchorY}px`);
                wheel.classList.add('on');
            }, 215);
        },
        setWheel: function (index, wheeli) {
            if (index > 0 && index <= 8) {
                this.hideWheel();
                setTimeout(function () {
                    if (wheeli == 1 || wheeli == 0) {
                        if (index == 1) {
                            this.showswal = true;
                            Swal.fire({
                                title: 'Wieviel Geld möchtest du geben?',
                                html: `<input type="text" id="itemvalue" maxlength="9" class="swal2-input" autofocus>`,
                                confirmButtonText: 'Weiter',
                                showCancelButton: true,
                                cancelButtonText: 'Abbrechen',
                                allowEnterKey: true,
                                allowOutsideClick: false,
                                closeOnClickOutside: false,
                                allowEscapeKey: true,
                                focusConfirm: false,
                                preConfirm: () => {
                                    this.dropvalue = Swal.getPopup().querySelector('#itemvalue').value
                                    if (!this.dropvalue || isNaN(this.dropvalue)) {
                                        Swal.showValidationMessage(`Bitte eine Menge eingeben!`)
                                    }
                                }
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    this.showswal = false;
                                    // eslint-disable-next-line no-undef
                                    mp.trigger("Client:UseWheel", "money", this.dropvalue);
                                } else {
                                    this.showswal = false;
                                    // eslint-disable-next-line no-undef
                                    mp.trigger("Client:HideWheel2");
                                }
                            })
                        } else if (index == 2) {
                            this.showswal = false;
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:UseWheel", "lock", 1);
                        } else if (index == 3) {
                            this.showswal = false;
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:Interact");
                        } else if (index == 4) {
                            this.showswal = false;
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:UseWheel", "open", 1);
                        } else if (index == 5) {
                            this.showswal = false;
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:UseWheel", "perso", 1);
                        } else if (index == 6) {
                            this.showswal = false;
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:UseWheel", "lics", 1);
                        } else if (index == 7) {
                            this.showswal = false;
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:UseWheel", "invehicle", 1);
                        } else if (index == 8) {
                            this.showswal = false;
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:UseWheel", "dog", 1);
                        } else {
                            this.showswal = false;
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:UseWheel", "nothing", 1);
                        }
                    } else if (wheeli == 2) {
                        this.showswal = false;
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:HideWheel2");
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:UseWheel", "setcloth", index);
                    } else if (wheeli == 3 || wheeli == 4 || wheeli == 5 || wheeli == 6) {
                        this.showswal = false;
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:HideWheel2");
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:UseWheel", "faction", index);
                    }
                }, 215);
            }
        }
    }
}
</script>

<style scoped>
html,
body,
template,
* {
    -webkit-user-select: none;
    -khtml-user-select: none;
    -moz-user-select: none;
    -o-user-select: none;
    user-select: none;
}

.centering {
    margin: 0;
    position: absolute;
    top: 50%;
    left: 50%;
    margin-right: -50%;
    transform: translate(-50%, -50%)
}

.centering2 {
    width: 100%;
    height: 100%;
    overflow: hidden;
    scrollbar-width: none;
    margin: auto;
    position: absolute;
    z-index: 1;
}

* {
    box-sizing: border-box;
}

html {
    user-select: none;
    overflow: hidden;
}

html body {
    background-color: transparent;
    width: 100vw;
    height: 100vh;
    overflow: hidden;
    position: relative;
    padding: 0;
    margin: 0;
}

h1 {
    position: absolute;
    top: 30%;
    color: rgba(255, 255, 255, 0.3);
    padding: 0 1rem;
    text-align: center;
    width: 100%;
}

.wheel {
    --x: 0px;
    --y: 0px;
    position: absolute;
    top: var(--y);
    left: var(--x);
    width: 500px;
    height: 500px;
    transform: translate(-50%, -50%);
    transform-origin: 0% 0%;
}

.wheel.on .arc {
    opacity: 0.8;
    transform: scale(1) rotate(var(--rotation)) !important;
    transition-timing-function: cubic-bezier(0, 0.5, 0.5, 1.5);
}

.wheel .arc {
    position: absolute;
    top: 0;
    right: 0;
    width: 50%;
    height: 50%;
    transform-origin: 0% 100%;
    background-image: radial-gradient(circle at 0% 100%, transparent, transparent 29.5%, var(--color-border) 30%, var(--color-border) 30.5%, var(--color) 31%, var(--color) 50%, var(--color-border) 50.25%, var(--color-border) 51.5%, transparent 51.75%, transparent);
    transition-property: transform, opacity, filter;
    transition-duration: 0.3s;
    transition-timing-function: cubic-bezier(0.4, -0.4, 0.7, -0.3);
    -webkit-clip-path: polygon(0 0, 0 99%, 99% 0);
    clip-path: polygon(0 0, 0 99%, 99% 0);
    opacity: 0;
}

.wheel .arc i {
    position: absolute;
    top: 40%;
    left: 15%;
    font-size: 2rem;
    color: white;
}

@media (max-width: 1920px) {
    .wheel .arc img {
        position: absolute;
        width: 2.5vw;
        top: 4.8vw;
        left: 1.6vw;
    }
}

@media (min-width: 2560px) {
    .wheel .arc img {
        position: absolute;
        height: 1.15vw;
        top: 4.6vh;
        left: 1.4vh;
    }
}

.wheel[data-chosen='1'] .arc:nth-child(1) {
    opacity: 1;
    transform: scale(1.1) rotate(var(--rotation)) !important;
    filter: brightness(150%);
}

.wheel[data-chosen='1'] .arc:nth-child(1) i {
    color: rgba(0, 0, 0, 0.5);
}

.wheel .arc:nth-child(1) {
    --rotation: -22.5deg;
    --color: #343A40;
    --color-border: #3F6791;
    transform: scale(0) rotate(var(--rotation));
    transition-delay: 0.015s;
}

.wheel .arc:nth-child(1) img {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
    opacity: 0.6;
}

.wheel .arc:nth-child(2) img {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
    opacity: 0.6;
}

.wheel .arc:nth-child(3) img {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
    opacity: 0.6;
}

.wheel .arc:nth-child(4) img {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
    opacity: 0.6;
}

.wheel .arc:nth-child(5) img {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
    opacity: 0.6;
}

.wheel .arc:nth-child(6) img {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
    opacity: 0.6;
}

.wheel .arc:nth-child(7) img {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
    opacity: 0.6;
}

.wheel .arc:nth-child(8) img {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
    opacity: 0.6;
}

.wheel .arc:nth-child(1) i {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
}

.wheel[data-chosen='2'] .arc:nth-child(2) {
    opacity: 1;
    transform: scale(1.1) rotate(var(--rotation)) !important;
    filter: brightness(150%);
}

.wheel[data-chosen='2'] .arc:nth-child(2) i {
    color: rgba(0, 0, 0, 0.5);
}

.wheel .arc:nth-child(2) {
    --rotation: 22.5deg;
    --color: #343A40;
    --color-border: #3F6791;
    transform: scale(0) rotate(var(--rotation));
    transition-delay: 0s;
}

.wheel .arc:nth-child(2) i {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
}

.wheel[data-chosen='3'] .arc:nth-child(3) {
    opacity: 1;
    transform: scale(1.1) rotate(var(--rotation)) !important;
    filter: brightness(150%);
}

.wheel[data-chosen='3'] .arc:nth-child(3) i {
    color: rgba(0, 0, 0, 0.5);
}

.wheel .arc:nth-child(3) {
    --rotation: 67.5deg;
    --color: #343A40;
    --color-border: #3F6791;
    transform: scale(0) rotate(var(--rotation));
    transition-delay: 0.015s;
}

.wheel .arc:nth-child(3) i {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
}

.wheel[data-chosen='4'] .arc:nth-child(4) {
    opacity: 1;
    transform: scale(1.1) rotate(var(--rotation)) !important;
    filter: brightness(150%);
}

.wheel[data-chosen='4'] .arc:nth-child(4) i {
    color: rgba(0, 0, 0, 0.5);
}

.wheel .arc:nth-child(4) {
    --rotation: 112.5deg;
    --color: #343A40;
    --color-border: #3F6791;
    transform: scale(0) rotate(var(--rotation));
    transition-delay: 0s;
}

.wheel .arc:nth-child(4) i {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
}

.wheel[data-chosen='5'] .arc:nth-child(5) {
    opacity: 1;
    transform: scale(1.1) rotate(var(--rotation)) !important;
    filter: brightness(150%);
}

.wheel[data-chosen='5'] .arc:nth-child(5) i {
    color: rgba(0, 0, 0, 0.5);
}

.wheel .arc:nth-child(5) {
    --rotation: 157.5deg;
    --color: #343A40;
    --color-border: #3F6791;
    transform: scale(0) rotate(var(--rotation));
    transition-delay: 0.015s;
}

.wheel .arc:nth-child(5) i {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
}

.wheel[data-chosen='6'] .arc:nth-child(6) {
    opacity: 1;
    transform: scale(1.1) rotate(var(--rotation)) !important;
    filter: brightness(150%);
}

.wheel[data-chosen='6'] .arc:nth-child(6) i {
    color: rgba(0, 0, 0, 0.5);
}

.wheel .arc:nth-child(6) {
    --rotation: 202.5deg;
    --color: #343A40;
    --color-border: #3F6791;
    transform: scale(0) rotate(var(--rotation));
    transition-delay: 0s;
}

.wheel .arc:nth-child(6) i {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
}

.wheel[data-chosen='7'] .arc:nth-child(7) {
    opacity: 1;
    transform: scale(1.1) rotate(var(--rotation)) !important;
    filter: brightness(150%);
}

.wheel[data-chosen='7'] .arc:nth-child(7) i {
    color: rgba(0, 0, 0, 0.5);
}

.wheel .arc:nth-child(7) {
    --rotation: 247.5deg;
    --color: #343A40;
    --color-border: #3F6791;
    transform: scale(0) rotate(var(--rotation));
    transition-delay: 0.015s;
}

.wheel .arc:nth-child(7) i {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
}

.wheel[data-chosen='8'] .arc:nth-child(8) {
    opacity: 1;
    transform: scale(1.1) rotate(var(--rotation)) !important;
    filter: brightness(150%);
}

.wheel[data-chosen='8'] .arc:nth-child(8) i {
    color: rgba(0, 0, 0, 0.5);
}

.wheel .arc:nth-child(8) {
    --rotation: 292.5deg;
    --color: #343A40;
    --color-border: #3F6791;
    transform: scale(0) rotate(var(--rotation));
    transition-delay: 0s;
}

.wheel .arc:nth-child(8) i {
    transform: rotate(calc(var(--rotation) * -1));
    color: rgba(255, 255, 255, 0.8);
    transition: color 0.3s;
}
</style>

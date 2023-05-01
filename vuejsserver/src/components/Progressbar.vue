<template>
<div class="progressbar" style="z-index: 1; overflow-x: auto; background-color:transparent; scrollbar-width: none;" v-if="progressBarShow">
    <div style="height: 100%; background-color: transparent;">
        <div class="row justify-content-center centering3">
            <div class="animate__animated animate__bounceInUp">
                <div class="card card-primary card-outline">
                    <div class="card-body">
                        <h5 class="text-center" style="color: white;font-family: 'Exo', sans-serif; font-size: 1.25vw">
                            {{progressBarText}}</h5>
                        <div class="progress" style="width: 35vw">
                            <div class="progress-barProgress progress-bar-striped progress-bar-animated bg-primary" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" :style="{ 'width': (progressCount > 0 ? parseInt(progressCount/100*100) : 0) + '%' }"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
export default {
    name: 'progressbar',
    data: function () {
        return {
            progressBarShow: false,
            progressCount: 0,
            progressTimer: null,
            progressBarText: '',
        }
    },
    mounted() {
        this.progressCount = 0;
        document.body.classList.add("dark-mode");
    },
    methods: {
        startProgressbar(time, action, text) {
            this.progressBarShow = true;
            this.progressBarText = text;
            var maxPlus = parseInt(100 / parseInt(time));
            var calculate = 0;
            var self = this;
            this.progressTimer = setInterval(function () {
                calculate = maxPlus + calculate;
                self.progressCount = calculate;
                if (self.progressCount >= 100) {
                    clearInterval(self.progressTimer);
                    self.progressTimer = null;
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:FinishProgress", action);
                }
            }, 1005);
        },
        stopProgressbar() {
            this.progressBarShow = false;
            this.progressCount = 0;
            if (this.progressTimer != null) {
                clearInterval(this.progressTimer);
                this.progressTimer = null;
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

.centering3 {
    width: 27.5vw;
    margin: 0;
    position: absolute;
    top: 88%;
    right: 1%;
    margin-left: -50%;
    transform: translate(-125%, -35%)
}
</style>

<template>
  <div class="card">
    <div class="row keep-item d-flex align-items-between m-2" :style="'background-image: url(' + keep.img + ')'">
      <div class="col-12 link-area px-0 pointer" @click="showKeep()">
        <button type="button" class="btn-invis full-height" data-toggle="modal" data-target="#keepModal">
        </button>
      </div>
      <div class="col-12 item-group d-flex align-items-center justify-content-between">
        <button class="keep-title mb-0 d-flex w-100 pointer btn-invis" @click="showKeep()" data-toggle="modal" data-target="#keepModal" type="button">
          {{ keep.name }}
        </button>
        <i class="fa fa-user-circle-o text-info pointer" aria-hidden="true" @click="sendToProfile(keep.creatorId)"></i>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { Keep } from '../models/Keep'
import { keepService } from '../services/KeepService'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
export default {
  props: {
    keep: { type: Object, default: () => new Keep() }
  },
  name: 'Keep',
  setup(props) {
    const router = useRouter()
    const state = reactive({
      activeKeep: computed(() => AppState.activeKeep)
    })
    return {
      state,
      showKeep() {
        keepService.setActiveKeep(props.keep)
      },
      sendToProfile(profileId) {
        router.push({ name: 'Profile', params: { profileId: profileId } })
      }
    }
  }
}
</script>

<style lang="scss">
.col {
    height: min-content;
    width: min-content;
}
.keep-item {
    width: 33vh;
    height: auto;
    max-height: 40vh;
    background-size: cover;
    border-radius: 10px;
    filter: drop-shadow(2px 2px 4px black);
}
.btn-invis {
  background-color: rgba(255, 255, 255, 0);
  border: none;
}
.full-height {
  min-height: 240px;
  height: 100%;
  width: 100%;
  padding-left: 0px !important;
  padding-right: 0px !important;
}
.item-group {
    height: min-content;
}
.link-area {
    min-height: 240px;
    height: 80%;
}
.keep-title {
    font-size: 20pt;
    font-weight: bold;
    color: var(--light);
    filter: drop-shadow(2px 2px 2px black);
}
.fa-user-circle-o {
    width: 24px;
    height: 24px;
    font-size: 18pt;
    background-color: black;
    border-radius: 100%;
    transition: all 0.3s ease-in-out;
}
.fa-user-circle-o:hover {
    transform: rotate(360deg);
}
.pointer {
  cursor: pointer;
}
</style>

<template>
  <div class="card">
    <div class="col-12 keep-item d-flex align-items-between m-2">
      <img :src="keep.img" alt="Keep Image" class="keep-img">
      <div class="row link-area m-0 px-0 pointer" @click="showKeep()">
        <button type="button" class="btn-invis" data-toggle="modal" data-target="#keepModal">
        </button>
      </div>
      <div class="row d-inline-flex item-group w-95 ml-2 mb-1 align-items-center justify-content-between">
        <button class="keep-title mb-0 d-flex pointer btn-invis" @click="showKeep()" data-toggle="modal" data-target="#keepModal" type="button">
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
.keep-item {
    width: 33vh;
    background-size: cover;
    border-radius: 10px;
    filter: drop-shadow(2px 2px 4px black);
}
.keep-img {
  width: 33vh;
  height: auto;
  border-radius: 10px;
}
.btn-invis {
  background-color: rgba(255, 255, 255, 0);
  border: none;
  width: 100%;
  padding: 0px 0px 0px 0px;
}
.full-height {
  height: 100%;
  width: 100%;
  padding-left: 0px !important;
  padding-right: 0px !important;
}
.item-group {
    height: min-content;
    position: absolute;
  bottom: 1%;
}
.w-95 {
  width: 95%;
}
.link-area {
    top: 0;
    height: 100%;
    width: 100%;
    position: absolute;
}
.keep-title {
    font-size: 12pt;
    font-weight: bold;
    width: 89.4%;
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
.card {
  background-color: rgba(255, 255, 255, 0);
  border: none;
}
</style>

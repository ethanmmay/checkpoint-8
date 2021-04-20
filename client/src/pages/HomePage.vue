<template>
  <div class="modal fade"
       id="keepModal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="keepModalTitle"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="row">
          <div class="col-6">
            <img :src="state.activeKeep.img" alt="Keep Image" class="img-fluid m-2">
          </div>
          <div class="col-6 d-flex h-450">
            <div class="row d-flex align-items-evenly">
              <div class="col w-400 d-flex align-items-center justify-content-between mt-2">
                <div class="col icon-group d-flex justify-content-center w-75">
                  <div class="icon text-gray">
                    <i class="fa fa-share text-info ml-5" aria-hidden="true"></i>
                    {{ state.activeKeep.shares }}
                  </div>
                  <div class="icon text-gray">
                    <i class="fa fa-eye text-info ml-2" aria-hidden="true"></i>
                    {{ state.activeKeep.views }}
                  </div>
                  <div class="icon text-gray">
                    <i class="fa fa-download text-info ml-2" aria-hidden="true"></i>
                    {{ state.activeKeep.keeps }}
                  </div>
                </div>
                <button type="button" class="close ml-5" data-dismiss="modal" aria-label="Close" id="keepModalClose">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="col mt-0">
                <h3 class="modal-title text-center pr-5  mt-2" id="keepModalLongTitle">
                  {{ state.activeKeep.name }}
                </h3>
                <div class="modal-body text-gray">
                  {{ state.activeKeep.description }}
                  <div class="modal-separator"></div>
                </div>
              </div>
              <div class="col w-400 mt-auto d-flex align-items-center justify-content-between p-2 mr-3" v-if="state.activeKeep.creator">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                  ADD TO VAULT
                </button>
                <i class="fa fa-trash fa-2x pointer" aria-hidden="true" v-if="state.activeKeep.creator.id == state.account.id" @click="deleteKeep(state.activeKeep.id)"></i>
                <div class="pointer" @click="sendToProfile(state.activeKeep.creator.id)">
                  <img :src="state.activeKeep.creator.picture" style="width: 40px; height: 40px;" alt="Creator's Profile Picture">
                  {{ state.activeKeep.creator.name.split('@')[0] }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="row mx-2 d-flex align-items-center">
    <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
import { useRouter } from 'vue-router'
export default {
  name: 'Home',
  setup() {
    const router = useRouter()
    onMounted(() => {
      keepService.getKeeps()
    })
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      activeKeep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account)
    })
    return {
      state,
      deleteKeep(keepId) {
        keepService.deleteKeep(keepId)
      },
      sendToProfile(profileId) {
        console.log('Send to profile')
        router.push({ name: 'Profile', params: { profileId: profileId } })
      }
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
@media only screen and (max-width: 412px) {
  .row {
      margin-left: 0px !important;
      margin-right: 0px !important;
  }
}
.close {
  margin-right: 3vh;
}
.modal-body {
  padding-left: 0px;
  margin-left: 3vh;
}
.text-gray {
  color: rgb(75, 75, 75);
}
.modal-separator {
  width: 90%;
  border-bottom: 2px solid rgb(75, 75, 75);
  margin-top: 3vh;
}
.h-450 {
  min-height: 450px;
}
.w-400 {
  min-width: 400px;
}
</style>

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
        <div class="row d-flex">
          <div class="col-sm-6">
            <img :src="state.activeKeep.img" alt="Keep Image" class="img-fluid m-2">
          </div>
          <div class="col-sm-6 d-flexbox align-items-between" v-if="state.activeKeep.creator">
            <div class="row">
              <div class="col-12">
                <div class="row d-inline-flex justify-content-between w-100 ml-0 pr-1">
                  <div class="placeholder">
                  </div>
                  <div class="icon-group">
                    <div class="icon">
                      <i class="fa fa-share text-info mr-1" aria-hidden="true"></i>
                      {{ state.activeKeep.shares }}
                    </div>
                    <div class="icon">
                      <i class="fa fa-eye text-info mr-1" aria-hidden="true"></i>
                      {{ state.activeKeep.views }}
                    </div>
                    <div class="icon">
                      <i class="fa fa-download text-info mr-1" aria-hidden="true"></i>
                      {{ state.activeKeep.keeps }}
                    </div>
                  </div>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close" id="keepModalClose">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
              </div>
              <div class="col-12">
                <div class="row d-block">
                  <h3 class="modal-title text-center" id="keepModalLongTitle">
                    {{ state.activeKeep.name }}
                  </h3>
                  <div class="modal-body text-gray">
                    {{ state.activeKeep.description }}
                    <div class="modal-separator"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="row d-flex justify-content-between m-0 keep-footer">
              <button type="button" class="btn btn-secondary" data-dismiss="modal">
                ADD TO VAULT
              </button>
              <i class="fa fa-trash fa-2x pointer" aria-hidden="true" v-if="state.activeKeep.creator.id == state.account.id" @click="deleteKeep(state.activeKeep.id)"></i>
              <div class="pointer" @click="sendToProfile(state.activeKeep.creator.id)">
                <img :src="state.activeKeep.creator.picture" style="width: 40px; height: 40px;" alt="Creator's Profile Picture">
                <span id="creatorName" class="ml-2">{{ state.activeKeep.creator.name.split('@')[0] }}</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="card-columns">
    <div v-for="keep in state.keeps" :key="keep.id">
      <Keep :keep="keep" />
    </div>
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
        document.getElementById('keepModalClose').click()
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
.modal-separator {
  width: 90%;
  border-bottom: 2px solid rgb(75, 75, 75);
  margin-top: 3vh;
}
.card-columns {
  //small
  @media(max-width: 800px) {
    column-count: 2;
  }
  @media(min-width: 1110px) {
    column-count: 4;
  }
  @media(min-width: 1200px) {
    column-count: 5;
  }
}
@media(max-width: 800px) {
    #creatorName {
      display: none;
    }
    #addToVaultButton {
      content: '';
    }
    .icon {
      margin-right: 0vh;
    }
    .img-fluid {
      margin: 0px 0px 0px 0px !important;
    }
  }
.remove-mr {
  margin: 0px 0px 0px 0px !important;
}
.icon {
  display: inline-flex;
  color: rgb(75, 75, 75);
  align-items: center;
  margin-right: 2vh;
}
.min-content {
  width: min-content;
  height: min-content;
}
.keep-footer {
  bottom: 1%;
  position: absolute;
  width: 90%;
}
.img-fluid {
  border: 4px solid white;
  border-radius: 10px;
}
</style>

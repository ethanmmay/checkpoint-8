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
                      {{ state.activeKeep.shares || 0 }}
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
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="row min-size w-100">
    <div class="col-12 min-size">
      <h2>{{ state.currentVault.name }}</h2>
    </div>
    <div class="col-12 min-size">
      Keeps: {{ state.keepsByVault.length}}
    </div>
    <div class="card-columns">
      <Keep v-for="keep in state.keepsByVault" :key="keep.id" :keep="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
import { vaultService } from '../services/VaultService'
import { useRoute } from 'vue-router'
export default {
  name: 'VaultPage',
  setup() {
    const route = useRoute()
    onMounted(() => {
      vaultService.getVaultById(route.params.vaultId)
      keepService.getKeepsByVaultId(route.params.vaultId)
    })
    const state = reactive({
      activeKeep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      currentVault: computed(() => AppState.currentVault),
      keepsByVault: computed(() => AppState.keepsByVault)
    })
    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">
.modal-separator {
  width: 90%;
  border-bottom: 2px solid rgb(75, 75, 75);
  margin-top: 3vh;
}
.card {
  background-color: rgba(255, 255, 255, 0) !important;
  border: none;
  width: min-content;
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
.min-size {
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

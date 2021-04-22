<template>
  <div class="private" v-if="vault.isPrivate">
    <div class="col d-inline-flex m-1 rounded bg-warning text-light p-2" v-if="route.params.profileId === state.account.id">
      <router-link :to="{ name: 'VaultPage', params: { vaultId: vault.id }}">
        <h5 class="mb-0">
          {{ vault.name }}
        </h5>
      </router-link>
      <i class="fa fa-times text-danger pointer ml-2" aria-hidden="true" @click="deleteVault(vault.id)" v-if="vault.creator.id === state.account.id"></i>
    </div>
  </div>
  <div class="public" v-else>
    <div class="col d-inline-flex m-1 rounded bg-dark text-light p-2">
      <router-link :to="{ name: 'VaultPage', params: { vaultId: vault.id }}">
        <h5 class="mb-0">
          {{ vault.name }}
        </h5>
      </router-link>
      <i class="fa fa-times text-danger pointer ml-2" aria-hidden="true" @click="deleteVault(vault.id)" v-if="vault.creator.id === state.account.id"></i>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { Vault } from '../models/Vault'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { vaultService } from '../services/VaultService'
export default {
  props: {
    vault: { type: Object, default: () => new Vault() }
  },
  name: 'Vault',
  setup(props) {
    const route = useRoute()
    const router = useRouter()
    const state = reactive({
      account: computed(() => AppState.account)
    })
    return {
      state,
      route,
      sendToVault(vaultId) {
        router.push({ name: 'VaultPage', params: { vaultId: vaultId } })
      },
      sendToProfile(profileId) {
        router.push({ name: 'Profile', params: { profileId: profileId } })
      },
      deleteVault(vaultId) {
        vaultService.deleteVault(vaultId)
      }
    }
  }
}
</script>

<style lang="scss">
.col {
  max-width: min-content;
}
.hidden {
  display: none !important;
}
</style>

<template>
  <div class="private" v-if="vault.isPrivate">
    <router-link :to="{ name: 'VaultPage', params: { vaultId: vault.id }}" v-if="route.params.profileId === state.account.id">
      <div class="col m-1 rounded bg-warning text-light p-2">
        <h5 class="mb-0">
          {{ vault.name }}
        </h5>
      </div>
    </router-link>
  </div>
  <div class="public" v-else>
    <router-link :to="{ name: 'VaultPage', params: { vaultId: vault.id }}">
      <div class="col m-1 rounded bg-dark text-light p-2">
        <h5 class="mb-0">
          {{ vault.name }}
        </h5>
      </div>
    </router-link>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { Vault } from '../models/Vault'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
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
        console.log('Send to profile')
        router.push({ name: 'Profile', params: { profileId: profileId } })
      }
    }
  }
}
</script>

<style lang="scss">
.col {
  max-width: min-content;
}
</style>

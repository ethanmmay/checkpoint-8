<template>
  <router-link :to="{ name: 'VaultPage', params: { vaultId: vault.id }}">
    <div class="col m-1 rounded bg-dark text-light p-2">
      <h5 class="mb-0">
        {{ vault.name }}
      </h5>
    </div>
  </router-link>
</template>

<script>
import { reactive } from 'vue'
import { Vault } from '../models/Vault'
import { useRouter } from 'vue-router'
export default {
  props: {
    vault: { type: Object, default: () => new Vault() }
  },
  name: 'Vault',
  setup(props) {
    const router = useRouter()
    const state = reactive({
    })
    return {
      state,
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

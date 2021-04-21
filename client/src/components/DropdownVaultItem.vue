<template>
  <a class="dropdown-item pointer" @click="addToVault(vault.id)">{{ vault.name }}</a>
</template>

<script>
import { computed, reactive } from 'vue'
import { vaultkeepService } from '../services/VaultKeepService'
import { AppState } from '../AppState'
export default {
  name: 'DropdownVaultItem',
  props: {
    vault: { type: Object, default: () => {} }
  },
  setup(props) {
    const state = reactive({
      activeKeep: computed(() => AppState.activeKeep)
    })
    return {
      props,
      addToVault(vaultId) {
        vaultkeepService.createRelationship(vaultId, state.activeKeep.id)
      }
    }
  }
}
</script>

<style>
.pointer {
    cursor: pointer;
}
</style>

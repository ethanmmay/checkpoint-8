<template>
  <div class="col-12">
    <div class="row d-inline-flex" v-if="state.profile">
      <img :src="state.profile.picture" alt="Profile Image" class="img-fluid">
      <div class="ml-3" v-if="state.profile.name">
        <h3>{{ state.profile.name.split('@')[0] }}</h3>
        <!-- <h6>Vaults: {{ state.profileVaults.length }}</h6> -->
        <h6>Keeps: {{ state.profileKeeps.length }}</h6>
      </div>
    </div>
    <div class="row">
      <h4>Vaults</h4>
    <!-- <Vault v-for="vault in state.profileVaults" :key="vault.id" :vault="vault" /> -->
    </div>
    <div class="row">
      <div class="col">
        <h4>Keeps</h4>
      </div>
      <div class="card-columns">
        <Keep v-for="keep in state.profileKeeps" :key="keep.id" :keep="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { profileService } from '../services/ProfileService'
export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    onMounted(() => {
      profileService.getProfile(route.params.profileId)
      profileService.getProfileVaults(route.params.profileId)
      profileService.getProfileKeeps(route.params.profileId)
    })
    const state = reactive({
      profile: computed(() => AppState.profile),
      profileVaults: computed(() => AppState.profileVaults),
      profileKeeps: computed(() => AppState.profileKeeps)
    })
    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">
img {
  max-width: 100px;
  max-height: 100px;
}
.card-columns {
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
</style>

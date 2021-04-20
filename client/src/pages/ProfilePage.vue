<template>
  <div>
    <img :src="state.profile.picture" alt="Profile Image">
    {{ state.profile.name }}
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
    })
    const state = reactive({
      profile: computed(() => AppState.profile)
    })
    return {
      state
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>

<template>
  <header>
    <Navbar />
  </header>
  <main class="container-fluid p-3">
    <router-view />
  </main>
  <footer>
    <div class="bg-dark text-light text-center p-4">
      Made with 💖 by CodeWorks
    </div>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
@media only screen and (max-width: 412px) {
  .container-fluid {
      padding-left: 0px !important;
      padding-right: 0px !important;
  }
}
</style>

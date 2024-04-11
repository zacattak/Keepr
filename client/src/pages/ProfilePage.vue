<template>
  <div class="container-fluid bg-info">

    <section class="row d-flex justify-content-evenly">
      <!-- <img :src="activeKeep.creator.picture" :alt="activeKeep.creator.name" class="creatorImg"> -->

      <!-- <div class="d-flex justify-content-evenly"> {{ activeKeep.creator.name }} </div> -->

    </section>
  </div>
</template>

<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
export default {
  setup() {
    // const activeKeep = computed(() => AppState.activeKeep)


    return {
      // activeKeep
    }
  }
}
</script>

<style scoped>
.container-fluid {
  /* background-color: green; */
  height: 100vh;
}

.creatorImg {
  width: 50%;
  border-radius: 100px;
  object-fit: cover;
  height: 20vh;
}
</style>

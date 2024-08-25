<template>
  <div class="container-fluid bg-info">
    <section class="row d-flex justify-content-evenly">

      <div v-for="keep in keeps" :key="keep.id" class="col-9 col-md-3 m-2 card mb-2 mt-2">

        <KeepComponent :keep="keep" />

      </div>

    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService.js'




export default {
  // props: {
  //       account: { type: Account, required: true }
  //   },

  setup() {

    async function getKeeps() {
      try {
        await keepsService.getKeeps()
      }
      catch (error) {
        Pop.error(error);
      }
    }

    onMounted(getKeeps)
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
.card {
  border: 2px solid black;
  border-radius: 16px;
  box-shadow: 3px 3px 10px rgba(42, 6, 134, 0.31);
}

// .home {
//   display: grid;
//   height: 80vh;
//   place-content: center;
//   text-align: center;
//   user-select: none;

//   .home-card {
//     width: clamp(500px, 50vw, 100%);

//     >img {
//       height: 200px;
//       max-width: 200px;
//       width: 100%;
//       object-fit: contain;
//       object-position: center;
//     }
//   }
// }</style>

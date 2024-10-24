<template>
  <div class="container-fluid">
    <section class="row">
      <div class="mx-auto masonry">

        <div v-for="keep in keeps" :key="keep.id" class="">

          <!-- <div v-for="keep in keeps" :key="keep.id" class="col-9 col-md-3 m-2 card mb-2 mt-2"> -->

          <KeepComponent :keep="keep" />

        </div>

      </div>
    </section>
  </div>

</template>

<script>
import { computed, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService.js'
// import { keepTagsService } from '../services/KeepTagsService';




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

    // async function getKeepTagsByKeepId(keepId) {
    //   try {
    //     await keepTagsService.getKeepTagsByKeepId(keepId)
    //   }
    //   catch (error) {
    //     Pop.error(error);
    //   }
    // }

    // async function fetchKeepsAndTags() {
    //   await getKeeps();

    //   for (const keep of AppState.keeps) {
    //     await getKeepTagsByKeepId(keep.id);
    //   }
    // }

    onMounted(getKeeps)
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
.container-fluid {
  background-color: #dcdcdc;
  /* Cooler, more modern gray */
  min-height: 100vh;
  /* Ensure the background covers the full page height */
}

.card {
  border: 2px solid black;
  border-radius: 16px;
  box-shadow: 3px 3px 10px rgba(42, 6, 134, 0.31);
}

.masonry {
  column-count: 3;
  /* Set the number of columns */
  column-gap: 1rem;
  /* Adjust gap between columns */


}

.masonry>div {
  break-inside: avoid;
  /* Prevent the item from breaking between columns */
  margin-bottom: 1rem;
  /* Add some spacing between items */
}

/* For tablets (medium screens), show 2 columns */
@media (max-width: 768px) {
  .masonry {
    column-count: 2;
    /* Reduce to 2 columns */
  }
}

/* For mobile phones (small screens), show 1 column */
@media (max-width: 576px) {
  .masonry {
    column-count: 1;
    /* Reduce to 1 column */
  }
}
</style>

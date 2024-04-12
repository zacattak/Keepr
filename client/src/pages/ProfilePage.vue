<template>
  <div class="container-fluid bg-info">

    <section class="row d-flex justify-content-evenly">
      <!-- 
      <div class="d-flex justify-content-center">

        <h1>Welcome {{ account.name }}</h1>


        <div class="">
          <img :src="account.coverImg" alt="">
        </div>
        <div class="">
          <img class="rounded" :src="account.picture" alt="" />
        </div>
      </div> -->


      <div class="col-12">

        <p>this is your profile page, I need to try to get vaults page working in time so it looks mid right now</p>
      </div>

      <div v-for="keep in keeps" :key="keep.id" class="col-9 col-md-3 m-2 card mb-2 mt-2">

        <KeepComponent :keep="keep" />

        <p>adfafdas</p>
      </div>

      <div class="col-12">

        <p>asdfasdf</p>
      </div>



    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import { useRoute } from 'vue-router';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop.js';
import { accountService } from '../services/AccountService.js';
import { vaultsService } from '../services/VaultsService.js';
import { keepsService } from '../services/KeepsService.js';

export default {
  setup() {
    const route = useRoute();
    async function getAccountById() {
      try {
        const accountId = route.params.accountId;
        logger.log('id had', accountId);
        await accountService.getAccountById(accountId);
      }
      catch (error) {
        Pop.error(error);
      }
    }

    async function getVaultsByAccountId() {
      try {
        const accountId = route.params.accountId;
        logger.log('fetching', accountId)
        await vaultsService.getVaultsByAccountId(accountId)
      }
      catch (error) {
        Pop.error(error);
      }
    }

    async function getKeepsByAccountId() {
      try {
        const accountId = route.params.accountId;
        logger.log('fetching keeps', accountId)
        await keepsService.getKeepsByAccountId(accountId)
      }
      catch (error) {
        Pop.error(error);
      }
    }
    // const activeKeep = computed(() => AppState.activeKeep)

    onMounted(() => {
      getAccountById();
      getVaultsByAccountId();
      getKeepsByAccountId();

    })
    return {
      account: computed(() => AppState.activeAccount),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
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

.card {
  border: 2px solid black;
  border-radius: 16px;
  box-shadow: 3px 3px 10px rgba(42, 6, 134, 0.31);
}

img {
  max-width: 100px;
}
</style>

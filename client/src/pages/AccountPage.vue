<template>
  <div class="container-fluid text-center">
    <h1>Welcome {{ account.name }}</h1>


    <div class="col-12">
      <img :src="account.coverImg" alt="">
    </div>

    <img class="rounded" :src="account.picture" alt="" />


    <!-- <p>{{ account.email }}</p> -->

    <form @submit.prevent="updateAccount()">
      <div class="form-group">
        <label for="name">Name</label>
        <input v-model="editableAccountData.name" type="name" class="form-control" id="name" aria-describedby="name"
          placeholder="Enter Name">
        <!-- <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small> -->
      </div>
      <div class="form-group">
        <label for="picture">Picture</label>
        <input v-model="editableAccountData.picture" type="picture" class="form-control" id="picture"
          placeholder="Picture">
      </div>
      <div class="form-group">
        <label for="coverImg">Cover Image</label>
        <input v-model="editableAccountData.coverImg" type="coverImg" class="form-control" id="coverImg"
          placeholder="Cover Image">
      </div>

      <button type="submit" class="btn btn-primary mt-2">Submit</button>
    </form>

  </div>
</template>

<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { accountService } from '../services/AccountService';
export default {
  setup() {

    const editableAccountData = ref({ name: '', picture: '', coverImg: '' })
    // coverImg: '' 
    return {
      account: computed(() => AppState.account),
      editableAccountData,
      async updateAccount() {
        try {
          await accountService.updateAccount(editableAccountData.value)
        }
        catch (error) {
          Pop.error(error);
        }
      }
    }
  }
}
</script>

<style scoped>
.container-fluid {
  background-color: #dcdcdc;
  /* Cooler, more modern gray */
  min-height: 100vh;
  /* Ensure the background covers the full page height */
}

img {
  max-width: 100px;
}

.container-fluid {
  /* background-color: green; */
  height: 100vh;
}
</style>

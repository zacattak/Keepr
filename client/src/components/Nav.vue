<template>
    <nav class="bg-dark">
        <section class="row">
            <div class="col-12 col-md-3 text-center">


                <div class="dropdown my-2">
                    <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton"
                        data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        Create
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                        <form class="dropdown-item px-4 py-3" @submit.prevent="createKeep()">
                            <div class="form-group">

                                <input v-model="editableKeepData.name" type="text" class="form-control" id="name"
                                    placeholder="Enter Keep Name" required maxlength="255">

                                <input v-model="editableKeepData.description" type="text" class="form-control"
                                    id="description" placeholder="Enter Keep Description" required maxlength="1000">

                                <input v-model="editableKeepData.img" type="text" class="form-control" id="img"
                                    placeholder="Enter Keep Image" required maxlength="1000">

                            </div>
                            <button type="submit" class="btn btn-primary">Create Keep</button>
                        </form>


                        <form class="dropdown-item px-4 py-3" @submit.prevent="createVault()">
                            <div class="form-group">

                                <input v-model="editableVaultData.name" type="text" class="form-control" id="vaultName"
                                    placeholder="Enter Vault Name" required maxlength="255">
                                <input v-model="editableVaultData.description" type="text" class="form-control"
                                    id="vaultDescription" placeholder="Enter Vault Description" required
                                    maxlength="1000">
                                <input v-model="editableVaultData.img" type="text" class="form-control" id="vaultImg"
                                    placeholder="Enter Vault Image" required maxlength="1000">


                            </div>
                            <button type="submit" class="btn btn-primary">Create Vault</button>
                        </form>

                        <form class="dropdown-item px-4 py-3" @submit.prevent="createPrivateVault()">
                            <div class="form-group">

                                <input v-model="editablePrivateVaultData.name" type="text" class="form-control"
                                    id="privateVaultName" placeholder="Enter Vault Name" required maxlength="255">
                                <input v-model="editablePrivateVaultData.description" type="text" class="form-control"
                                    id="privateVaultDescription" placeholder="Enter Vault Description" required
                                    maxlength="1000">
                                <input v-model="editablePrivateVaultData.img" type="text" class="form-control"
                                    id="privateVaultImg" placeholder="Enter Vault Img" required maxlength="1000">

                            </div>
                            <button type="submit" class="btn btn-danger">Create Private Vault</button>
                        </form>

                    </div>
                </div>


            </div>

            <div class="col-12 col-md-3 text-center">

                <router-link class="" :to="{ name: 'Home' }">
                    <p class="my-3 text-info">Chamber of Keeps</p>
                </router-link>

            </div>

            <div class="col-12 col-md-3 text-center">
                <router-link class="" :to="{ name: 'Vaults' }">
                    <p class="my-3 text-info">My Vaults</p>
                </router-link>

            </div>

            <div class="col-12 col-md-3 text-center">
                <p class="my-2">
                    <Login />
                </p>
            </div>

        </section>

    </nav>
</template>


<script>
import Login from './Login.vue';
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService';

import { ref } from 'vue';
import Pop from '../utils/Pop';
import { Account } from '../models/Account';
export default {

    setup() {
        const editableKeepData = ref({ name: '', description: '', img: '' })

        const editableVaultData = ref({ name: '', description: '', img: '' })

        const editablePrivateVaultData = ref({ name: '', description: '', img: '', isPrivate: true })

        function refreshPage() {
            window.location.reload();
        }

        return {
            refreshPage,

            editableKeepData,

            editableVaultData,

            editablePrivateVaultData,

            async createPrivateVault() {

                try {
                    const privateVault = await vaultsService.createPrivateVault(editablePrivateVaultData.value)
                    editablePrivateVaultData.value = { name: '', description: '', img: '', isPrivate: true }
                    Pop.success(`${privateVault.name} has been created`)
                }
                catch (error) {
                    Pop.error(error);
                }

            },

            async createVault() {
                try {
                    const vault = await vaultsService.createVault(editableVaultData.value)
                    editableVaultData.value = { name: '', description: '', img: '' }
                    Pop.success(`${vault.name} has been created`)
                }
                catch (error) {
                    Pop.error(error);
                }
            },

            async createKeep() {
                try {
                    const keep = await keepsService.createKeep(editableKeepData.value)
                    editableKeepData.value = { name: '', description: '', img: '' }
                    Pop.success(`${keep.name} has been created`)
                }
                catch (error) {
                    Pop.error(error);
                }
            }


        }
    },

    components: { Login }
}
</script>


<style lang="scss" scoped>
.bg-dark {
    background-color: #f0e6d2 !important;
    /* Beige-cream background */
}
</style>
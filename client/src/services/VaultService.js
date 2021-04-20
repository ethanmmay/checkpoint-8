/* eslint-disable no-undef */
import { AppState } from '../AppState'
import { Vault } from '../models/Vault'
import router from '../router'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultService {
  async getVaults() {
    try {
      const res = await api.get('api/vaults')
      AppState.vaults = res.data.map(k => new Vault(k))
    } catch (err) {
      logger.error('Couldnt load Vaults', err)
    }
  }

  async getVaultById(vaultId) {
    try {
      const res = await api.get('api/vaults/' + vaultId)
      AppState.currentVault = new Vault(res.data)
    } catch (err) {
      logger.error('Couldnt load Vaults', err)
    }
  }

  setActiveVault(vault) {
    try {
      AppState.activeVault = vault
    } catch (error) {
      logger.error('Cant set active vault', error)
    }
  }

  async createVault() {
    try {
      Swal.fire({
        title: 'New Vault',
        html: '<input type="text" id="title" class="swal2-input" placeholder="Enter Vault Name.. "><textarea type="text" id="body" class="swal2-input pt-2" placeholder="Describe the vault..."></textarea>',
        confirmButtonText: 'Report',
        focusConfirm: false,
        preConfirm: () => {
          const title = Swal.getPopup().querySelector('#title').value
          const body = Swal.getPopup().querySelector('#body').value
          if (!title || !body) {
            Swal.showValidationMessage('Please enter title and body')
          }
          return { title: title, body: body }
        }
      }).then(async(result) => {
        const newVault = {
          title: result.value.title,
          description: result.value.body,
          creatorId: AppState.user.id
        }
        await api.post('api/vaults', newVault)
        AppState.vaults = []
        await this.getVaults()
        router.push({ name: 'Vault', params: { id: AppState.vaults[AppState.vaults.length - 1].id } })
      })
    } catch (err) {
      logger.error('Couldnt create Vault', err)
    }
  }

  async deleteVault(vaultId) {
    try {
      if (window.confirm('Are you sure you want to delete your Vault?')) {
        await api.delete('api/vaults/' + vaultId)
        this.getVaults()
        document.getElementById('vaultModalClose').click()
      }
    } catch (error) {
      logger.log(error)
    }
  }

  sortVaults() {
    AppState.vaults.sort(function(a, b) { return a.closed - b.closed })
  }

  async editVault(vault) {
    try {
      Swal.fire({
        title: 'Edit Vault',
        html: `<input type="text" id="title" class="swal2-input" placeholder="Enter Vault Name.. " value="${vault.title}"><textarea type="text" id="body" class="swal2-input pt-2" placeholder="Describe the vault...">${vault.description}</textarea>`,
        confirmButtonText: 'Save',
        focusConfirm: false,
        preConfirm: () => {
          const title = Swal.getPopup().querySelector('#title').value
          const body = Swal.getPopup().querySelector('#body').value
          if (!title || !body) {
            Swal.showValidationMessage('Please enter title and body')
          }
          return { title: title, body: body }
        }
      }).then(async(result) => {
        const newVault = {
          title: result.value.title,
          description: result.value.body
        }
        await api.put('api/vaults/' + vault.id, newVault)
        AppState.vaults = []
        await this.getVaults()
      })
    } catch (err) {
      logger.error('Couldnt edit Vault', err)
    }
  }
}

export const vaultService = new VaultService()

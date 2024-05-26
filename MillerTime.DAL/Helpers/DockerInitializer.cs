using Docker.DotNet;
using Docker.DotNet.Models;
using System.Diagnostics;

namespace MillerTime.DAL.Helpers
{
    public static class DockerInitializer
    {

        private static readonly string containerName = "MillerTimeDB";
        private static readonly string imageName = "miller-time-db";
        private static readonly string dockerFileDirectory = "MillerTime.DAL";
        private static readonly DockerClient client;

        static DockerInitializer()
        {
            client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
        }

        public static async Task StartDocker() 
        {
            await startDockerContainer();
        }

        private static async Task<ContainerListResponse> getDockerContainer() 
        {
            try
            {
                var containers = await client.Containers.ListContainersAsync(new ContainersListParameters { All = true });
                var container = containers.FirstOrDefault(c => c.Names.Contains($"/{containerName}"));
                if (container == null)
                {
                    var imageExists = await createDockerImage();
                    if (imageExists)
                    {
                        var imagePort = "1433";
                        await client.Containers.CreateContainerAsync(new CreateContainerParameters
                        {
                            Image = $"{imageName}:dockerfile",
                            Name = containerName,
                            ExposedPorts = new Dictionary<string, EmptyStruct>()
                            {
                                { imagePort, new EmptyStruct() }
                            },
                            HostConfig = new HostConfig()
                            {
                                PortBindings = new Dictionary<string, IList<PortBinding>>
                                {
                                    { imagePort, new List<PortBinding> { new PortBinding { HostPort = imagePort } } }
                                }
                            }
                        });
                        containers = await client.Containers.ListContainersAsync(new ContainersListParameters { All = true });
                        container = containers.FirstOrDefault(c => c.Names.Contains($"/{containerName}"));
                    }
                }
                return container;
            }
            catch (TimeoutException)
            {
                throw new InvalidOperationException("Docker Desktop is not currently running");
            }
        }

        private static async Task<bool> createDockerImage() 
        {
            var existingImages = await client.Images.ListImagesAsync(new ImagesListParameters() { All = true });
            if (existingImages.Any(i => i.RepoTags.Contains($"{imageName}:dockerfile")))
            {
                return true;
            }
            else 
            {
                var dockerFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, dockerFileDirectory);
                var process = new Process();
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C docker build -t {imageName}:dockerfile {dockerFilePath}",
                    UseShellExecute = true,
                };
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
                existingImages = await client.Images.ListImagesAsync(new ImagesListParameters() { All = true });
                return existingImages.Any(i => i.RepoTags.Contains($"{imageName}:dockerfile"));
            }
        }

        private static async Task startDockerContainer() 
        {
            var client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
            var dbContainer = await getDockerContainer();
            if (dbContainer != null)
            {
                await client.Containers.StartContainerAsync(dbContainer.ID, new ContainerStartParameters());
            }
            else
            {
                throw new Exception("There was a problem starting the docker container. Try running it manually");
            }
        }
    }
}
